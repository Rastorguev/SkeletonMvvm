using Xamarin.Forms;
using XFormsSkeleton.ViewModels;
using XFormsSkeleton.Views;

namespace XFormsSkeleton
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            ServiceLocator.RegisterDependencies();

            //MainPage = new NavigationPage(new ViewD());
        }

        protected override async void OnStart()
        {
            base.OnStart();

            var navService = ServiceLocator.Resolve<INavigationService>();//new NavigationService();
            await navService.NavigateToAsync<MainViewModel>();
        }
    }
}