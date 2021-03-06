using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SkeletonMvvm
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

        public Task PushAsync<TViewModel>(bool modal = false, bool newNavigation = false, bool animated = true)
            where TViewModel : IBaseViewModel
        {
            return InternalPushAsync<TViewModel>(modal, newNavigation, animated);
        }

        public Task PushAsync<TViewModel, TNavData>(TNavData navData, bool modal = false, bool newNavigation = false,
            bool animated = true)
            where TViewModel : IBaseViewModel<TNavData>
        {
            return InternalPushAsync<TViewModel, TNavData>(navData, newNavigation, false, animated);
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

        public Task PopToRootAsync(bool animated = true)
        {
            var currentNavigation = CurrentNavigation;

            return currentNavigation.PopToRootAsync(animated);
        }

        public Task TryPopToAsync<TViewModel>(bool animated = true)
        {
            var currentNavigation = CurrentNavigation;
            var navigationStack = currentNavigation.NavigationStack;

            if (!navigationStack.Any())
            {
                return Task.FromResult(false);
            }

            var targetPage = navigationStack.LastOrDefault(p => p.BindingContext is TViewModel);
            var lastPage = navigationStack.Last();
            if (targetPage == null || targetPage == lastPage)
            {
                return Task.FromResult(false);
            }

            var lastButOnePage = navigationStack[navigationStack.Count - 2];
            while (lastButOnePage != targetPage)
            {
                currentNavigation.RemovePage(lastButOnePage);
                lastButOnePage = navigationStack[navigationStack.Count - 2];
            }

            return currentNavigation.PopAsync(animated);
        }

        public void SetAsRoot<TViewModel>(bool newNavigation = false) where TViewModel : IBaseViewModel
        {
            if (Application.Current == null)
            {
                throw new Exception("Application.Current is null");
            }

            var page = _pageResolver.ResolvePage<TViewModel>();
            if (newNavigation)
            {
                page = new NavigationPage(page);
            }

            Application.Current.MainPage = page;
        }

        public void SetAsRoot<TViewModel, TNavData>(TNavData navData, bool newNavigation = false)
            where TViewModel : IBaseViewModel<TNavData>
        {
            if (Application.Current == null)
            {
                throw new Exception("Application.Current is null");
            }

            var page = _pageResolver.ResolvePage<TViewModel, TNavData>(navData);
            if (newNavigation)
            {
                page = new NavigationPage(page);
            }

            Application.Current.MainPage = page;
        }

        private async Task InternalPushAsync<TViewModel>(bool modal,
            bool newNavigation, bool animated)
            where TViewModel : IBaseViewModel
        {
            var page = _pageResolver.ResolvePage<TViewModel>();

            await PushPage(page, modal, newNavigation, animated);
        }

        private async Task InternalPushAsync<TViewModel, TNavData>(TNavData navData, bool modal,
            bool newNavigation, bool animated)
            where TViewModel : IBaseViewModel<TNavData>
        {
            var page = _pageResolver.ResolvePage<TViewModel, TNavData>(navData);

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

        private Page GetCurrentPage()
        {
            var root = Application.Current.MainPage;
            var modalStack = root.Navigation.ModalStack;

            if (modalStack.Any())
            {
                return FindTopPage(modalStack.Last());
            }

            return FindTopPage(root);
        }

        private Page FindTopPage(Page page)
        {
            var navigationPage = page as NavigationPage;
            if (navigationPage != null)
            {
                var navStack = navigationPage.Navigation.NavigationStack;
                if (navStack.Any())
                {
                    return FindTopPage(navStack.Last());
                }
            }

            var masterDetailPage = page as MasterDetailPage;
            if (masterDetailPage?.Detail != null)
            {
                return FindTopPage(masterDetailPage.Detail);
            }

            var tabbedPage = page as TabbedPage;
            if (tabbedPage?.CurrentPage != null)
            {
                return FindTopPage(tabbedPage.CurrentPage);
            }

            return page;
        }
    }
}