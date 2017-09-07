﻿namespace Sample.Core.Views.MasterDetail
{
    public class MenuItem
    {
        public string Title { get; set; }
        public MenuItemType ItemType{ get; set; }
    }

    public enum MenuItemType
    {
        Type1,
        Type2,
        Type3
    }
}