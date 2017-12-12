//---------------------------------------------------------------------------
//
// <copyright file="Season2DetailPage.xaml.cs" company="Microsoft">
//    Copyright (C) 2015 by Microsoft Corporation.  All rights reserved.
// </copyright>
//
// <createdOn>8/12/2016 9:52:16 AM</createdOn>
//
//---------------------------------------------------------------------------

using System;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using AppStudio.DataProviders.YouTube;
using BarbieApp.Sections;
using BarbieApp.Navigation;
using BarbieApp.ViewModels;
using AppStudio.Uwp;

namespace BarbieApp.Pages
{
    public sealed partial class Season2DetailPage : Page
    {
        private DataTransferManager _dataTransferManager;

        public Season2DetailPage()
        {
            ViewModel = ViewModelFactory.NewDetail(new Season2Section());
			this.ViewModel.ShowInfo = false;
            this.InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Disabled;
            commandBar.DataContext = ViewModel;
        }

        public DetailViewModel ViewModel { get; set; }        

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            await ViewModel.LoadStateAsync(e.Parameter as NavDetailParameter);

            _dataTransferManager = DataTransferManager.GetForCurrentView();
            _dataTransferManager.DataRequested += OnDataRequested;
            ShellPage.Current.SupportFullScreen = true;

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _dataTransferManager.DataRequested -= OnDataRequested;
            ShellPage.Current.SupportFullScreen = false;

            base.OnNavigatedFrom(e);
        }

        private void OnDataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            ViewModel.ShareContent(args.Request);
        }
    }
}
