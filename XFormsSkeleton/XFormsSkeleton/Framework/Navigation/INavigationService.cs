using System.Threading.Tasks;
using XFormsSkeleton.Framework.ViewModels;

namespace XFormsSkeleton.Framework.Navigation
{
    public interface INavigationService
    {
        Task PushAsync<TViewModel>(bool modal = false, bool animated = true)
            where TViewModel : IBaseViewModel;

        Task PushAsync<TViewModel, TNavData>(TNavData navData, bool modal = false, bool animated = true)
            where TViewModel : IBaseViewModel<TNavData>;

        Task PushWithNewNavigationAsync<TViewModel>(bool modal = false, bool animated = true)
            where TViewModel : IBaseViewModel;

        Task PushWithNewNavigationAsync<TViewModel, TNavData>(TNavData navData, bool modal = false,
            bool animated = true)
            where TViewModel : IBaseViewModel<TNavData>;

        Task PopAsync(bool modal = false, bool animated = true);

        Task PopToRootAsync(bool animated = true);

        Task TryPopToAsync<TViewModel>(bool animated = true);
    }
}