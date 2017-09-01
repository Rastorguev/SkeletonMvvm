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
        Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase;
    }

    public class NavigationService : INavigationService
    {
        public NavigationPage MainNavPage => Application.Current.MainPage as NavigationPage;

        public Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), null);
        }

        private async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            var viewModel = Activator.CreateInstance(viewModelType);

            var page = CreatePage(viewModelType, parameter);
            page.BindingContext = viewModel;

            if (Application.Current.MainPage == null)
            {
                Application.Current.MainPage = new NavigationPage();
            }

            await MainNavPage.PushAsync(page);
        }

        private static Page CreatePage(Type viewModelType, object parameter)
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