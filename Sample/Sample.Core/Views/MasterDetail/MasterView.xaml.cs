using Xamarin.Forms;

namespace Sample.Core.Views.MasterDetail
{
    public partial class MasterView : ContentPage
    {
        public ListView List { get; }

        public MasterView()
        {
            InitializeComponent();

            List = MenuItemsListView;
        }
    }
}