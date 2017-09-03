using Xamarin.Forms;
using XFormsSkeleton.Framework;
using XFormsSkeleton.ViewModels;
using XFormsSkeleton.Views;

namespace XFormsSkeleton
{
    public partial class App : Application
    {
        private readonly INavigationService _navigationService;

        public App()
        {
            var serviceLocator = new ServiceLocator();
            serviceLocator.RegisterDependencies();

            _navigationService = serviceLocator.Resolve<INavigationService>();

            InitializeComponent();
            MainPage = new NavigationPage(new MainView());
        }

        protected override async void OnStart()
        {
            base.OnStart();

            await _navigationService.NavigateToAsync<MainViewModel>();
        }
    }
}