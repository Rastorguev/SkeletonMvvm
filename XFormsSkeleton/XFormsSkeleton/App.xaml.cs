using Xamarin.Forms;
using XFormsSkeleton.Framework;
using XFormsSkeleton.ViewModels.MasterDetail;

namespace XFormsSkeleton
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var serviceLocator = new ServiceLocator();
            serviceLocator.RegisterDependencies();

            var pageResolver = serviceLocator.Resolve<IPageResolver>();
            MainPage = pageResolver.ResolvePage<MasterDetailViewModel>();

            //new NavigationPage(pageResolver.ResolvePage<MainViewModel>());
        }
    }
}