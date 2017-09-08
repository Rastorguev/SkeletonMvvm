using System.Collections.ObjectModel;
using Sample.Core.Views.MasterDetail;
using SkeletonMvvm;

namespace Sample.Core.ViewModels.MasterDetail
{
    public class MasterViewModel : IBaseViewModel
    {
        public ObservableCollection<MenuItem> MenuItems { get; } = new ObservableCollection<MenuItem>(new[]
        {
            new MenuItem
            {
                Title = "Items",
                ItemType = MenuItemType.ItemsList
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