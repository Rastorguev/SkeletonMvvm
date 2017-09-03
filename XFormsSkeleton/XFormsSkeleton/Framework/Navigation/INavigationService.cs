﻿using System.Threading.Tasks;
using Xamarin.Forms;

namespace XFormsSkeleton.Framework.Navigation
{
    public interface INavigationService
    {
        void Start<TViewModel>(Application application) where TViewModel : BaseViewModel<object>;

        Task NavigateToAsync<TViewModel>(bool modal = false, bool animated = true)
            where TViewModel : BaseViewModel<object>;

        Task NavigateToAsync<TViewModel, TNavData>(TNavData navData, bool modal = false, bool animated = true)
            where TViewModel : BaseViewModel<TNavData>;

        Task PopAsync(bool modal = false, bool animated = true);
    }
}