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

        public override Task Navigate()
        {
            return _navigationService.PushWithNewNavigationAsync<AViewModel, string>("test");
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
            return _navigationService.PushWithNewNavigationAsync<BViewModel, string>("test");
        }
    }

    public class BViewModel : SimpleViewModel<string>
    {
        private readonly INavigationService _navigationService;
        private string _navData;

        public BViewModel(INavigationService navigationService)
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
            //return Task.FromResult(false);

            return _navigationService.PushWithNewNavigationAsync<CViewModel, string>("test", modal: true);
        }
    }

    public class CViewModel : SimpleViewModel<string>
    {
        private readonly INavigationService _navigationService;
        private string _navData;

        public CViewModel(INavigationService navigationService)
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
            //return Task.FromResult(false);

            return _navigationService.PushWithNewNavigationAsync<DViewModel, string>("test");
        }
    }

    public class DViewModel : SimpleViewModel<string>
    {
        private readonly INavigationService _navigationService;
        private string _navData;

        public DViewModel(INavigationService navigationService)
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
            //return Task.FromResult(false);

            return _navigationService.PushAsync<MainViewModel>();
        }
    }

}