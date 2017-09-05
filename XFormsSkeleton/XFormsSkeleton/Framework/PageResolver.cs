﻿using System;
using System.Globalization;
using System.Reflection;
using Xamarin.Forms;
using XFormsSkeleton.Framework.ViewModels;

namespace XFormsSkeleton.Framework
{
    public static class PageResolver
    {
        public static Page ResolvePage(IBaseViewModel viewModel)
        {
            var page = CreatePage(viewModel.GetType());
            page.BindingContext = viewModel;

            return page;
        }

        public static Page CreatePage(Type viewModelType)
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