//---------------------------------------------------------------------------
//
// <copyright file="Season1ListPage.xaml.cs" company="Microsoft">
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
    public sealed partial class Season1ListPage : Page
    {
	    public ListViewModel ViewModel { get; set; }
        public Season1ListPage()
        {
			ViewModel = ViewModelFactory.NewList(new Season1Section());

            this.InitializeComponent();
			commandBar.DataContext = ViewModel;
            NavigationCacheMode = NavigationCacheMode.Disabled;

        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
			ShellPage.Current.ShellControl.SelectItem("b15c9d18-5de5-4a4f-ad11-a16c7a3ebc91");
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
