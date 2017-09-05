using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using XFormsSkeleton.Framework.ViewModels;

namespace XFormsSkeleton.Framework.Navigation
{
    public class NavigationService : INavigationService
    {
        private readonly IPageResolver _pageResolver;

        public NavigationService(IPageResolver pageResolver)
        {
            _pageResolver = pageResolver;
        }

        public INavigation CurrentNavigation
        {
            get
            {
                var currentPage = GetCurrentPage();
                var currentNavigation = currentPage.Navigation;

                Debug.WriteLine($"------- {currentPage.GetType().Name}");

                return currentNavigation;
            }
        }

        public Task PushAsync<TViewModel>(bool modal = false, bool animated = true)
            where TViewModel : IBaseViewModel
        {
            return PushAsync<TViewModel>(modal, false, animated);
        }

        public Task PushAsync<TViewModel, TNavData>(TNavData navData, bool modal = false, bool animated = true)
            where TViewModel : IBaseViewModel<TNavData>
        {
            return PushAsync<TViewModel, TNavData>(navData, modal, false, animated);
        }

        public Task PushWithNewNavigationAsync<TViewModel>(bool modal = false, bool animated = true)
            where TViewModel : IBaseViewModel
        {
            return PushAsync<TViewModel>(modal, true, animated);
        }

        public Task PushWithNewNavigationAsync<TViewModel, TNavData>(TNavData navData, bool modal = false,
            bool animated = true) where TViewModel : IBaseViewModel<TNavData>
        {
            return PushAsync<TViewModel, TNavData>(navData, modal, true, animated);
        }

        public Task PopAsync(bool modal = false, bool animated = true)
        {
            var currentNavigation = CurrentNavigation;
            if (modal)
            {
                return currentNavigation.PopModalAsync(animated);
            }

            return currentNavigation.PopAsync(animated);
        }

        private async Task PushAsync<TViewModel>(bool modal,
            bool newNavigation, bool animated)
            where TViewModel : IBaseViewModel
        {
            var page = _pageResolver.ResolvePage<TViewModel>();

            await PushPage(page, modal, newNavigation, animated);
        }

        private async Task PushAsync<TViewModel, TNavData>(TNavData navData, bool modal,
            bool newNavigation, bool animated)
            where TViewModel : IBaseViewModel<TNavData>
        {
            var page = _pageResolver.ResolvePage<TViewModel>();

            await PushPage(page, modal, newNavigation, animated);
        }

        private async Task PushPage(Page page, bool modal, bool newNavigation, bool animated)
        {
            if (newNavigation)
            {
                page = new NavigationPage(page);
            }

            var currentNavigation = CurrentNavigation;
            if (modal)
            {
                await currentNavigation.PushModalAsync(page, animated);
            }
            else
            {
                await currentNavigation.PushAsync(page, animated);
            }
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