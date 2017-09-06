using System.Linq;
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
            master.Title = "Menu";
            Master = master;

            var firstMenuItem = MasterView.List.ItemsSource.Cast<MenuItem>().First();
            var detail = CreatePage(firstMenuItem);
            Detail = new NavigationPage(detail);

            MasterView.List.ItemSelected += ListView_ItemSelected;
        }

        private MasterView MasterView => Master as MasterView;

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MenuItem;
            if (item == null)
            {
                return;
            }

            var page = CreatePage(item);

            Detail = new NavigationPage(page);
            IsPresented = false;

            MasterView.List.SelectedItem = null;
        }

        private Page CreatePage(MenuItem item)
        {
            Page page = null;

            switch (item.ItemType)
            {
                case MenuItemType.Type1:
                    page = _pageResolver.ResolvePage<AViewModel, string>("A");
                    break;
                case MenuItemType.Type2:
                    page = _pageResolver.ResolvePage<BViewModel, string>("B");
                    break;
                case MenuItemType.Type3:
                    page = _pageResolver.ResolvePage<CViewModel, string>("C");
                    break;
                default:
                    page = _pageResolver.ResolvePage<DViewModel, string>("D");
                    break;
            }

            page.Title = item.Title;
            return page;
        }
    }
}