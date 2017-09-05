using Xamarin.Forms;
using XFormsSkeleton.Framework;
using XFormsSkeleton.ViewModels;
using XFormsSkeleton.ViewModels.MasterDetail;

namespace XFormsSkeleton.Views.MasterDetail
{
    public class MasterDetailView : MasterDetailPage
    {
        private readonly IPageResolver _pageResolver;

        public MasterDetailView(IPageResolver pageResolver)
        {
            _pageResolver = pageResolver;

            var master = pageResolver.ResolvePage<MasterViewModel>();
            master.Title = "Master";
            var detail = CreatePage(MasterMenuItemType.Type1);
            detail.Title = "Test";

            Master = master;
            Detail = new NavigationPage(detail);

            MasterView.List.ItemSelected += ListView_ItemSelected;
        }

        private MasterView MasterView => Master as MasterView;

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterMenuItem;
            if (item == null)
            {
                return;
            }

            var page = CreatePage(item.ItemType);
            page.Title = item.Title;

            Detail = new NavigationPage(page);
            IsPresented = false;

            MasterView.List.SelectedItem = null;
        }

        private Page CreatePage(MasterMenuItemType itemType)
        {
            Page page = null;

            switch (itemType)
            {
                case MasterMenuItemType.Type1:
                    page = _pageResolver.ResolvePage<AViewModel, string>("A");
                    break;
                case MasterMenuItemType.Type2:
                    page = _pageResolver.ResolvePage<BViewModel, string>("B");
                    break;
                case MasterMenuItemType.Type3:
                    page = _pageResolver.ResolvePage<CViewModel, string>("C");
                    break;
                default:
                    page = _pageResolver.ResolvePage<DViewModel, string>("D");
                    break;
            }

            return page;
        }
    }
}