using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace XFormsSkeleton
{
    public abstract partial class SimplePage : ContentPage
    {
        protected SimplePage()
        {
            InitializeComponent();

            TitleLabel.Text = GetType().Name;
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

    public class MainPage : SimplePage
    {
        public override void Navigate()
        {
            var pageA = new PageA();
            pageA.Init("test");

            Navigation.PushAsync(pageA);
        }
    }

    public class PageA : SimplePage
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
            Navigation.PushModalAsync(new NavigationPage(new PageB()));
        }
    }

    public class PageB : SimplePage
    {
        public override void Navigate()
        {
           Navigation.PushModalAsync(new PageC());
        }
    }

    public class PageC: SimplePage
    {
        public override void Navigate()
        {
            Navigation.PushModalAsync(new PageD());
        }
    }

    public class PageD : SimplePage
    {
        public override void Navigate()
        {
            //Navigation.PushAsync(new NavigationPage(new PageE()));
        }
    }

    public class PageE : SimplePage
    {
        public override void Navigate()
        {
        }
    }
}