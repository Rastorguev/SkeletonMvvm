using System.Threading.Tasks;
using System.Windows.Input;
using SkeletonMvvm;
using Xamarin.Forms;

namespace Sample.Core.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IBackgroundTask _backgroundTask;

        public LoginViewModel(INavigationService navigationService, IBackgroundTask backgroundTask)
        {
            _navigationService = navigationService;
            _backgroundTask = backgroundTask;
        }

        public string Login { get; set; }

        public string Password { get; set; }

        public ICommand LoginCommand => new Command(async () => await DoLogin());

        private Task DoLogin()
        {
            if (!_backgroundTask.IsRunning)
            {
                _backgroundTask.Start();
            }
            else
            {
                _backgroundTask.Stop();
            }

            return Task.FromResult(false);
            //return _navigationService.PushAsync<TermsAndConditionsViewModel>();
        }
    }
}