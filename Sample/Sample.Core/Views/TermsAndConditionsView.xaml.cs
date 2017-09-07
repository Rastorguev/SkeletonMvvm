using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sample.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermsAndConditionsView: ContentPage
    {
        public TermsAndConditionsView()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}