﻿using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XFormsSkeleton.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : ContentPage
    {
        public LoginView()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}