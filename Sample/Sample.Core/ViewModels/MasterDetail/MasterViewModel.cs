using System.Collections.ObjectModel;
using SkeletonMvvm;
using XFormsSkeleton.Views.MasterDetail;

namespace XFormsSkeleton.ViewModels.MasterDetail
{
    public class MasterViewModel : IBaseViewModel
    {
        public ObservableCollection<MenuItem> MenuItems { get; }

        public MasterViewModel()
        {
            MenuItems = new ObservableCollection<MenuItem>(new[]
            {
                new MenuItem
                {
                    Title = "AView",
                    ItemType = MenuItemType.Type1
                },

                new MenuItem
                {
                    Title = "BView",
                    ItemType = MenuItemType.Type2
                },

                new MenuItem
                {
                    Title = "CView",
                    ItemType = MenuItemType.Type3
                }
            });
        }
    }
}