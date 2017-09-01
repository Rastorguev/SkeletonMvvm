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
        Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel<object>;

        Task NavigateToAsync<TViewModel, TNavData>(TNavData navData) where TViewModel : BaseViewModel<TNavData>;
    }

    public class NavigationService : INavigationService
    {
        public NavigationPage MainNavPage => Application.Current.MainPage as NavigationPage;

        public Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel<object>
        {
            return InternalNavigateToAsync<TViewModel, object>(null);
        }

        public Task NavigateToAsync<TViewModel, TNavData>(TNavData navData) where TViewModel : BaseViewModel<TNavData>
        {
            return InternalNavigateToAsync<TViewModel, TNavData>(navData);
        }

        private async Task InternalNavigateToAsync<TViewModel, TNavData>(TNavData navData)
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

            await MainNavPage.PushAsync(page);
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