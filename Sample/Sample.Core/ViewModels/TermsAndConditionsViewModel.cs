using System.Threading.Tasks;
using System.Windows.Input;
using SkeletonMvvm;
using Xamarin.Forms;

namespace Sample.Core.ViewModels
{
    public class TermsAndConditionsViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;

        public TermsAndConditionsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public string Title { get; } = "Terms and Conditions";
        public string TermsAndConditions { get; } = "Terms and Conditions";

        public ICommand AcceptCommand => new Command(async () => await DoAccept());
        public ICommand CancelCommand => new Command(async () => await DoCancel());

        private Task DoAccept()
        {
            return _navigationService.PushAsync<MainViewModel>();
        }

        private Task DoCancel()
        {
            return _navigationService.PushAsync<MainViewModel>();
        }
    }
}