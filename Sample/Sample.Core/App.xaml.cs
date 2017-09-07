using SkeletonMvvm;
using Xamarin.Forms;
using XFormsSkeleton.ViewModels;
using XFormsSkeleton.ViewModels.Tabs;

namespace XFormsSkeleton
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var serviceLocator = new AutofacServiceLocator();
            serviceLocator.RegisterDependencies();

            var pageResolver = serviceLocator.Resolve<IPageResolver>();
            MainPage = new NavigationPage(pageResolver.ResolvePage<MainViewModel>());
            //MainPage = pageResolver.ResolvePage<MasterDetailViewModel>();
            //MainPage = new NavigationPage(pageResolver.ResolvePage<TabbedViewModel>());
        }
    }
}