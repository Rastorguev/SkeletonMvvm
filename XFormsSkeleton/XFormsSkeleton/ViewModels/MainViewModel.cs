using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XFormsSkeleton.ViewModels.Base;

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
            await _navigationService.NavigateToAsync<AViewModel, string>("test");
        }
    }

    public class AViewModel : SimpleViewModel<string>
    {
        private string _navData;

        public AViewModel()
        {
            _navData = "";
        }

        public override Task InitAsync(string navData)
        {
            _navData = navData;

            return base.InitAsync(navData);
        }

        public override Task Navigate()
        {
            return Task.FromResult(false);
        }
    }
}