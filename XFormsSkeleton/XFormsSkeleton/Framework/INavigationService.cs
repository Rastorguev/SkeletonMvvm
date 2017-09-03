using System.Threading.Tasks;

namespace XFormsSkeleton.Framework
{
    public interface INavigationService
    {
        Task NavigateToAsync<TViewModel>(bool modal = false, bool animated = true)
            where TViewModel : BaseViewModel<object>;

        Task NavigateToAsync<TViewModel, TNavData>(TNavData navData, bool modal = false, bool animated = true)
            where TViewModel : BaseViewModel<TNavData>;

        Task PopAsync(bool modal = false, bool animated = true);
    }
}