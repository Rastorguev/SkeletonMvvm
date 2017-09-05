﻿using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XFormsSkeleton.Framework.Navigation;
using XFormsSkeleton.Framework.ViewModels;

namespace XFormsSkeleton.ViewModels
{
    public abstract class SimpleViewModel : BaseViewModel
    {
        public ICommand NavigateCommand => new Command(async () => await Navigate());

        public abstract Task Navigate();
    }

    public abstract class SimpleViewModel<TNavData> : SimpleViewModel, IBaseViewModel<TNavData>
    {
        public virtual void Init(TNavData navData)
        {
        }
    }

    public class MainViewModel : SimpleViewModel
    {
        private readonly INavigationService _navigationService;

        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override Task Navigate()
        {
            return _navigationService.PushWithNewNavigationAsync<AViewModel, string>("test");
        }
    }

    public class AViewModel : SimpleViewModel<string>
    {
        private readonly INavigationService _navigationService;
        private string _navData;

        public AViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _navData = "";
        }

        public override void Init(string navData)
        {
            _navData = navData;
        }

        public override Task Navigate()
        {
            return _navigationService.PushAsync<BViewModel, string>("test");
        }
    }

    public class BViewModel : SimpleViewModel<string>
    {
        private readonly INavigationService _navigationService;
        private string _navData;

        public BViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _navData = "";
        }

        public override void Init(string navData)
        {
            _navData = navData;
        }

        public override Task Navigate()
        {
            return _navigationService.PushWithNewNavigationAsync<CViewModel, string>("test", modal: true);
        }
    }

    public class CViewModel : SimpleViewModel<string>
    {
        private readonly INavigationService _navigationService;
        private string _navData;

        public CViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _navData = "";
        }

        public override void Init(string navData)
        {
            _navData = navData;
        }

        public override Task Navigate()
        {
            return _navigationService.PushWithNewNavigationAsync<DViewModel, string>("test");
        }
    }

    public class DViewModel : SimpleViewModel<string>
    {
        private readonly INavigationService _navigationService;
        private string _navData;

        public DViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _navData = "";
        }

        public override void Init(string navData)
        {
            _navData = navData;
        }

        public override Task Navigate()
        {
            return _navigationService.PushAsync<MainViewModel>();
        }
    }
}