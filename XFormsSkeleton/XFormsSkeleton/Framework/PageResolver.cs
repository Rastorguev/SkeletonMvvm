using System;
using System.Globalization;
using System.Reflection;
using Xamarin.Forms;
using XFormsSkeleton.Framework.ViewModels;

namespace XFormsSkeleton.Framework
{
    public class PageResolver : IPageResolver
    {
        private readonly IServiceLocator _serviceLocator;

        public PageResolver(IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
        }

        public Page ResolvePage<TViewModel>() where TViewModel : IBaseViewModel
        {
            var viewModel = _serviceLocator.Resolve<TViewModel>();
            var page = CreatePage(viewModel.GetType());
            page.BindingContext = viewModel;

            return page;
        }

        public Page ResolvePage<TViewModel, TNavData>(TNavData navData)
            where TViewModel : IBaseViewModel<TNavData>
        {
            var viewModel = _serviceLocator.Resolve<TViewModel>();
            var page = CreatePage(viewModel.GetType());
            page.BindingContext = viewModel;

            viewModel.Init(navData);

            return page;
        }

        private static Page CreatePage(Type viewModelType)
        {
            var pageType = GetPageTypeForViewModel(viewModelType);
            if (pageType == null)
            {
                throw new Exception($"Cannot locate page type for {viewModelType}");
            }

            var page = Activator.CreateInstance(pageType) as Page;
            return page;
        }

        private static Type GetPageTypeForViewModel(Type viewModelType)
        {
            var viewName = viewModelType.FullName.Replace("Model", string.Empty);
            var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
            var viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewName,
                viewModelAssemblyName);
            var viewType = Type.GetType(viewAssemblyName);

            return viewType;
        }
    }
}