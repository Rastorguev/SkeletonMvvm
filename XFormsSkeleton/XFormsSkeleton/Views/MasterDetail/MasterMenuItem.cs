using System;
using Xamarin.Forms;

namespace XFormsSkeleton.Views.MasterDetail
{
    public class MasterMenuItem
    {
        public string Title { get; set; }
        public MasterMenuItemType ItemType{ get; set; }
    }

    public enum MasterMenuItemType
    {
        Type1,
        Type2,
        Type3
    }
}