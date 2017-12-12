//---------------------------------------------------------------------------
//
// <copyright file="Season6ListPage.xaml.cs" company="Microsoft">
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
    public sealed partial class Season6ListPage : Page
    {
	    public ListViewModel ViewModel { get; set; }
        public Season6ListPage()
        {
			ViewModel = ViewModelFactory.NewList(new Season6Section());

            this.InitializeComponent();
			commandBar.DataContext = ViewModel;
            NavigationCacheMode = NavigationCacheMode.Disabled;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
			ShellPage.Current.ShellControl.SelectItem("bd2f29ad-be9a-452d-85a7-3ed7cce9b28c");
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
