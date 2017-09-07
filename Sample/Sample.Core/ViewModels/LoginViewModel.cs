using System.Threading.Tasks;
using System.Windows.Input;
using SkeletonMvvm;
using Xamarin.Forms;

namespace XFormsSkeleton.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;

        public LoginViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public ICommand LoginCommand => new Command(async () => await Login());

        private Task Login()
        {
            return _navigationService.PushAsync<MainViewModel>();
        }
    }
}