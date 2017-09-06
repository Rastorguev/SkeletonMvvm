using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFormsSkeleton.Framework;
using XFormsSkeleton.ViewModels;

namespace XFormsSkeleton.Views.Tabs
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