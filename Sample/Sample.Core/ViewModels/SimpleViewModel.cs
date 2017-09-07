using System.Threading.Tasks;
using System.Windows.Input;
using SkeletonMvvm;
using Xamarin.Forms;

namespace XFormsSkeleton.ViewModels
{
    public abstract class SimpleViewModel : BaseViewModel
    {
        public ICommand NavigateCommand => new Command(async () => await Navigate());

        public abstract Task Navigate();
    }

    public abstract class SimpleViewModel<TNavData> : SimpleViewModel, IBaseViewModel<TNavData>
    {
        public virtual void Init(TNavData navData)
        {
        }
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
            return _navigationService.PushAsync<AViewModel, string>("test");
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

        public override void Init(string navData)
        {
            _navData = navData;
        }

        public override Task Navigate()
        {

            return _navigationService.PushAsync<BViewModel, string>("test");
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

        public override void Init(string navData)
        {
            _navData = navData;
        }

        public override Task Navigate()
        {
            return _navigationService.PushAsync<CViewModel, string>("test");
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

        public override void Init(string navData)
        {
            _navData = navData;
        }

        public override Task Navigate()
        {
            return _navigationService.PushAsync<DViewModel, string>("test");
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

        public override void Init(string navData)
        {
            _navData = navData;
        }

        public override Task Navigate()
        {
            return _navigationService.TryPopToAsync<MainViewModel>();
        }
    }
}