using Sample.Core.ViewModels;
using SkeletonMvvm;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sample.Core.Views.Tabs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedView : TabbedPage
    {
        public TabbedView(IPageResolver pageResolver)
        {
            InitializeComponent();

            Children.Add(pageResolver.ResolvePage<AViewModel>());
            Children.Add(pageResolver.ResolvePage<BViewModel>());
        }
    }
}