using System.Collections.ObjectModel;
using XFormsSkeleton.Framework.ViewModels;
using XFormsSkeleton.Views.MasterDetail;

namespace XFormsSkeleton.ViewModels.MasterDetail
{
    public class MasterViewModel : IBaseViewModel
    {
        public ObservableCollection<MasterMenuItem> MenuItems { get; }

        public MasterViewModel()
        {
            MenuItems = new ObservableCollection<MasterMenuItem>(new[]
            {
                new MasterMenuItem
                {
                    Title = "AView",
                    ItemType = MasterMenuItemType.Type1
                },

                new MasterMenuItem
                {
                    Title = "BView",
                    ItemType = MasterMenuItemType.Type2
                },

                new MasterMenuItem
                {
                    Title = "CView",
                    ItemType = MasterMenuItemType.Type3
                }
            });
        }
    }
}