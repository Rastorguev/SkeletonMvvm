using System.Linq;
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

        public INavigation CurrentNavigation => GetCurrentPage().Navigation;

        public void Start<TViewModel>(Application application) where TViewModel : BaseViewModel<object>
        {
            var viewModelType = typeof(TViewModel);
            var viewModel = _serviceLocator.Resolve<TViewModel>();

            var page = PageUtils.CreatePage(viewModelType);
            page.BindingContext = viewModel;

            application.MainPage = new NavigationPage(page);
            viewModel.InitAsync(null).Wait();
        }

        public Task PushAsync<TViewModel>(bool modal = false, bool animated = true)
            where TViewModel : BaseViewModel<object>
        {
            return InternalNavigateToAsync<TViewModel, object>(null, modal, false, animated);
        }

        public Task PushAsync<TViewModel, TNavData>(TNavData navData, bool modal = false, bool animated = true)
            where TViewModel : BaseViewModel<TNavData>
        {
            return InternalNavigateToAsync<TViewModel, TNavData>(navData, modal, false, animated);
        }

        public Task PushWithNewNavigationAsync<TViewModel>(bool modal = false, bool animated = true)
            where TViewModel : BaseViewModel<object>
        {
            return InternalNavigateToAsync<TViewModel, object>(null, modal, true, animated);
        }

        public Task PushWithNewNavigationAsync<TViewModel, TNavData>(TNavData navData, bool modal = false,
            bool animated = true) where TViewModel : BaseViewModel<TNavData>
        {
            return InternalNavigateToAsync<TViewModel, TNavData>(navData, modal, true, animated);
        }

        public Task PopAsync(bool modal = false, bool animated = true)
        {
            if (modal)
            {
                return CurrentNavigation.PopModalAsync(animated);
            }

            return CurrentNavigation.PopAsync(animated);
        }

        private async Task InternalNavigateToAsync<TViewModel, TNavData>(TNavData navData, bool modal,
            bool newNavigation, bool animated)
            where TViewModel : BaseViewModel<TNavData>
        {
            var viewModelType = typeof(TViewModel);
            var viewModel = _serviceLocator.Resolve<TViewModel>();

            var page = PageUtils.CreatePage(viewModelType);
            page.BindingContext = viewModel;

            if (newNavigation)
            {
                page = new NavigationPage(page);
            }

            if (modal)
            {
                await CurrentNavigation.PushModalAsync(page, animated);
            }
            else
            {
                await CurrentNavigation.PushAsync(page, animated);
            }

            await viewModel.InitAsync(navData);
        }

        public Page GetCurrentPage()
        {
            var root = Application.Current.MainPage;
            var modalStack = root.Navigation.ModalStack;

            if (modalStack.Any())
            {
                return FindTopPage(modalStack.Last());
            }

            return FindTopPage(root);
        }

        public Page FindTopPage(Page page)
        {
            var navigationStack = page.Navigation.NavigationStack;
            var childNavigationPage = (NavigationPage) navigationStack.FirstOrDefault(p => p is NavigationPage);

            if (childNavigationPage == null || childNavigationPage.Navigation == page.Navigation)
            {
                if (navigationStack.Any())
                {
                    return navigationStack.Last();
                }
                return page;
            }

            return FindTopPage(childNavigationPage);
        }
    }
}