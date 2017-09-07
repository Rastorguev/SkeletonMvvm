﻿using Sample.Core.ViewModels;
using SkeletonMvvm;
using Xamarin.Forms;

namespace Sample.Core
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var serviceLocator = new AutofacServiceLocator();
            serviceLocator.RegisterDependencies();

            var pageResolver = serviceLocator.Resolve<IPageResolver>();
            MainPage = new NavigationPage(pageResolver.ResolvePage<LoginViewModel>());
            //MainPage = pageResolver.ResolvePage<MasterDetailViewModel>();
            //MainPage = new NavigationPage(pageResolver.ResolvePage<TabbedViewModel>());
        }
    }
}