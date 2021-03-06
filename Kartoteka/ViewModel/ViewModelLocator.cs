﻿/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:Kartoteka.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using Kartoteka.Model;

namespace Kartoteka.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<IDataService, Design.DesignDataService>();
            }
            else
            {
                SimpleIoc.Default.Register<IDataService, DataService>();
            }

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<AuthorViewModel>();
            SimpleIoc.Default.Register<BookViewModel>();
            SimpleIoc.Default.Register<SearchViewModel>();
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }
        public AuthorViewModel Author
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AuthorViewModel>();
            }
        }
        public BookViewModel Book
        {
            get
            {
                return ServiceLocator.Current.GetInstance<BookViewModel>();
            }
        }
        public SearchViewModel Search
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SearchViewModel>();
            }
        }

        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
            var search = SimpleIoc.Default.GetInstance<SearchViewModel>();
            search.Dispose();
        }
    }
}