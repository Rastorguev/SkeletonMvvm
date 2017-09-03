using System;
using System.Diagnostics;
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

        public void Save(string prefix)
        {
        }

        public void Restore(string prefix)
        {
        }
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

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Debug.WriteLine("------OnAppearing");
            Debug.WriteLine($"-----{_id}");
            Debug.WriteLine("------------------------------");
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            Debug.WriteLine("------OnSizeAllocated");
            Debug.WriteLine($"-----{_id}");
            Debug.WriteLine("------------------------------");
        }

        public void Init(string id)
        {
            _id = id;
        }

        public override void Navigate()
        {
            Navigation.PushModalAsync(new NavigationPage(new ViewB()));
        }
    }

    public class ViewB : SimpleView
    {
        public override void Navigate()
        {
            Navigation.PushModalAsync(new ViewC());
        }
    }

    public class ViewC : SimpleView
    {
        public override void Navigate()
        {
            Navigation.PushModalAsync(new ViewD());
        }
    }

    public class ViewD : SimpleView
    {
        public override void Navigate()
        {
            //Navigation.PushAsync(new NavigationView(new ViewE()));
        }
    }

    public class ViewE : SimpleView
    {
        public override void Navigate()
        {
        }
    }
}