using Xamarin.Forms;
using XFormsSkeleton.Framework.Navigation;
using XFormsSkeleton.ViewModels;

namespace XFormsSkeleton
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var serviceLocator = new ServiceLocator();
            serviceLocator.RegisterDependencies();

            var navigationService = serviceLocator.Resolve<INavigationService>();
            navigationService.Start<MainViewModel>(this);
        }
    }
}