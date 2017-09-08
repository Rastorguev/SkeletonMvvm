using Sample.Core.ViewModels;
using SkeletonMvvm;
using Xamarin.Forms;

namespace Sample.Core.Views.Tabs
{
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