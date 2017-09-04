using System;
using Xamarin.Forms;

namespace XFormsSkeleton.Views
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

        public virtual void NavButton_OnClicked(object sender, EventArgs e)
        {
            Navigate();
        }

        public abstract void Navigate();
    }

    public class MainView : SimpleView
    {
        public override void Navigate()
        {
            var ViewA = new AView();
            ViewA.Init("test");

            Navigation.PushAsync(ViewA);
        }
    }

    public class AView : SimpleView
    {
        private string _id;

        public void Init(string id)
        {
            _id = id;
        }

        public override void Navigate()
        {
            Navigation.PushModalAsync(new NavigationPage(new BView()));
        }
    }

    public class BView : SimpleView
    {
        public override void Navigate()
        {
            Navigation.PushModalAsync(new CView());
        }
    }

    public class CView : SimpleView
    {
        public override void Navigate()
        {
            Navigation.PushModalAsync(new DView());
        }
    }

    public class DView : SimpleView
    {
        public override void Navigate()
        {
        }
    }

    public class ViewE : SimpleView
    {
        public override void Navigate()
        {
        }
    }
}