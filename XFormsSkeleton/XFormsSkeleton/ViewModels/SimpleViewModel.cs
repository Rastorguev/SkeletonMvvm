using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XFormsSkeleton.Framework;
using XFormsSkeleton.Framework.Navigation;

namespace XFormsSkeleton.ViewModels
{
    public abstract class SimpleViewModel<TNavData> : BaseViewModel<TNavData>
    {
        public ICommand NavigateCommand => new Command(async () => await Navigate());

        public abstract Task Navigate();
    }

    public abstract class SimpleViewModel : SimpleViewModel<object>
    {
    }

    public class MainViewModel : SimpleViewModel
    {
        private readonly INavigationService _navigationService;

        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override async Task Navigate()
        {
            await _navigationService.NavigateToAsync<AViewModel, string>("test", modal: true);
        }
    }

    public class AViewModel : SimpleViewModel<string>
    {
        private readonly INavigationService _navigationService;
        private string _navData;

        public AViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _navData = "";
        }

        public override Task InitAsync(string navData)
        {
            _navData = navData;

            return base.InitAsync(navData);
        }

        public override Task Navigate()
        {
            return _navigationService.PopAsync(modal: true);
        }
    }
}