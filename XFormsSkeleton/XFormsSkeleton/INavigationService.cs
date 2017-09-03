using System;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;
using XFormsSkeleton.ViewModels.Base;

namespace XFormsSkeleton
{
    public interface INavigationService
    {
        Task NavigateToAsync<TViewModel>(bool modal = false, bool animated = true)
            where TViewModel : BaseViewModel<object>;

        Task NavigateToAsync<TViewModel, TNavData>(TNavData navData, bool modal = false, bool animated = true)
            where TViewModel : BaseViewModel<TNavData>;

        Task PopAsync(bool modal = false, bool animated = true);
    }

    public class NavigationService : INavigationService
    {
        public INavigation Navigation => Application.Current.MainPage.Navigation;

        public Task NavigateToAsync<TViewModel>(bool modal = false, bool animated = true)
            where TViewModel : BaseViewModel<object>
        {
            return InternalNavigateToAsync<TViewModel, object>(null, modal, animated);
        }

        public Task NavigateToAsync<TViewModel, TNavData>(TNavData navData, bool modal = false, bool animated = true)
            where TViewModel : BaseViewModel<TNavData>
        {
            return InternalNavigateToAsync<TViewModel, TNavData>(navData, modal, animated);
        }

        public Task PopAsync(bool modal = false, bool animated = true)
        {
            if (modal)
            {
                return Navigation.PopModalAsync(animated);
            }

            return Navigation.PopAsync(animated);
        }

        private async Task InternalNavigateToAsync<TViewModel, TNavData>(TNavData navData, bool modal, bool animated)
            where TViewModel : BaseViewModel<TNavData>
        {
            var viewModelType = typeof(TViewModel);
            var viewModel = ServiceLocator.Resolve<TViewModel>();

            var page = CreatePage(viewModelType);
            page.BindingContext = viewModel;

            if (Application.Current.MainPage == null)
            {
                Application.Current.MainPage = new NavigationPage();
            }

            if (modal)
            {
                await Navigation.PushModalAsync(page, animated);
            }
            else
            {
                await Navigation.PushAsync(page, animated);
            }

            await viewModel.InitAsync(navData);
        }

        private static Page CreatePage(Type viewModelType)
        {
            var pageType = GetPageTypeForViewModel(viewModelType);
            if (pageType == null)
            {
                throw new Exception($"Cannot locate page type for {viewModelType}");
            }

            var page = Activator.CreateInstance(pageType) as Page;
            return page;
        }

        private static Type GetPageTypeForViewModel(Type viewModelType)
        {
            var viewName = viewModelType.FullName.Replace("Model", string.Empty);
            var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
            var viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewName,
                viewModelAssemblyName);
            var viewType = Type.GetType(viewAssemblyName);

            return viewType;
        }
    }
}