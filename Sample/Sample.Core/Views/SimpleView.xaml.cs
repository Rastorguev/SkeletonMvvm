using Xamarin.Forms;

namespace Sample.Core.Views
{
    public abstract partial class SimpleView : ContentPage
    {
        protected SimpleView()
        {
            InitializeComponent();

            var title = GetType().Name;
            Title = title;
            TitleLabel.Text = title;
        }
    }

    public class MainView : SimpleView
    {
    }

    public class AView : SimpleView
    {
    }

    public class BView : SimpleView
    {
    }

    public class CView : SimpleView
    {
    }

    public class DView : SimpleView
    {
    }

    public class ViewE : SimpleView
    {
    }
}