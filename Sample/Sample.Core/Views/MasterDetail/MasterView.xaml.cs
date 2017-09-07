using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XFormsSkeleton.Views.MasterDetail
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
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