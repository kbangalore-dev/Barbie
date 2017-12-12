//---------------------------------------------------------------------------
//
// <copyright file="Season5ListPage.xaml.cs" company="Microsoft">
//    Copyright (C) 2015 by Microsoft Corporation.  All rights reserved.
// </copyright>
//
// <createdOn>8/12/2016 9:52:16 AM</createdOn>
//
//---------------------------------------------------------------------------

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;
using AppStudio.DataProviders.YouTube;
using BarbieApp.Sections;
using BarbieApp.ViewModels;
using AppStudio.Uwp;

namespace BarbieApp.Pages
{
    public sealed partial class Season5ListPage : Page
    {
	    public ListViewModel ViewModel { get; set; }
        public Season5ListPage()
        {
			ViewModel = ViewModelFactory.NewList(new Season5Section());

            this.InitializeComponent();
			commandBar.DataContext = ViewModel;
            NavigationCacheMode = NavigationCacheMode.Disabled;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
			ShellPage.Current.ShellControl.SelectItem("881416ff-c375-4fdf-8592-87af00afe85c");
			ShellPage.Current.ShellControl.SetCommandBar(commandBar);
			if (e.NavigationMode == NavigationMode.New)
            {			
				await this.ViewModel.LoadDataAsync();
                this.ScrollToTop();
			}			
            base.OnNavigatedTo(e);
        }

    }
}
