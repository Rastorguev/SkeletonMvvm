using System.Threading.Tasks;

namespace SkeletonMvvm
{
    public interface INavigationService
    {
        Task PushAsync<TViewModel>(bool modal = false, bool newNavigation = false, bool animated = true)
            where TViewModel : IBaseViewModel;

        Task PushAsync<TViewModel, TNavData>(TNavData navData, bool newNavigation = false, bool modal = false,
            bool animated = true)
            where TViewModel : IBaseViewModel<TNavData>;

        Task PopAsync(bool modal = false, bool animated = true);

        Task PopToRootAsync(bool animated = true);

        Task TryPopToAsync<TViewModel>(bool animated = true);

        void SetAsRoot<TViewModel>(bool newNavigation = false)
            where TViewModel : IBaseViewModel;

        void SetAsRoot<TViewModel, TNavData>(TNavData navData, bool newNavigation = false)
            where TViewModel : IBaseViewModel<TNavData>;
    }
}