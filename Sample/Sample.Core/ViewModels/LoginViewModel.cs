using System.Threading.Tasks;
using System.Windows.Input;
using SkeletonMvvm;
using Xamarin.Forms;

namespace Sample.Core.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;

        public LoginViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public string Login { get; set; }

        public string Password { get; set; }

        public ICommand LoginCommand => new Command(async () => await PerformLogin());

        private Task PerformLogin()
        {
            return _navigationService.PushAsync<MainViewModel>();
        }
    }
}