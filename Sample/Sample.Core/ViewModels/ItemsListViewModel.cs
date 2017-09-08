using System.Collections.ObjectModel;
using System.Linq;
using SkeletonMvvm;

namespace Sample.Core.ViewModels
{
    public class ItemsListViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;

        public ItemsListViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            FillItemsList();
        }

        private void FillItemsList()
        {
            var count = 1000;
            Items = new ObservableCollection<ItemViewModel>(Enumerable.Range(0, count)
                .Select(i => new ItemViewModel($"Item {i}", $"Description {i}")));
        }

        public ObservableCollection<ItemViewModel> Items { get; private set; }
    }

    public class ItemViewModel
    {
        public string Title { get; }
        public string Details { get; }

        public ItemViewModel(string title, string details)
        {
            Title = title;
            Details = details;
        }
    }
}