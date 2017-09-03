using System.Threading.Tasks;
using Xamarin.Forms;

namespace XFormsSkeleton.Framework.Navigation
{
    public class NavigationService : INavigationService
    {
        private readonly IServiceLocator _serviceLocator;

        public NavigationService(IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
        }

        public INavigation Navigation => Application.Current.MainPage.Navigation;

        public void Start<TViewModel>(Application application) where TViewModel : BaseViewModel<object>
        {
            var viewModelType = typeof(TViewModel);
            var viewModel = _serviceLocator.Resolve<TViewModel>();

            var page = PageUtils.CreatePage(viewModelType);
            page.BindingContext = viewModel;

            application.MainPage = new NavigationPage(page);
            viewModel.InitAsync(null).Wait();
        }

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
            var viewModel = _serviceLocator.Resolve<TViewModel>();

            var page = PageUtils.CreatePage(viewModelType);
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
    }
}