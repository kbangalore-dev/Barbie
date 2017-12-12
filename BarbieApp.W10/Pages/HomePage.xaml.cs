//---------------------------------------------------------------------------
//
// <copyright file="HomePage.xaml.cs" company="Microsoft">
//    Copyright (C) 2015 by Microsoft Corporation.  All rights reserved.
// </copyright>
//
// <createdOn>8/12/2016 9:52:16 AM</createdOn>
//
//---------------------------------------------------------------------------

using System.Windows.Input;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;

using AppStudio.Uwp;
using AppStudio.Uwp.Commands;
using AppStudio.Uwp.Navigation;
using Microsoft.Advertising.WinRT.UI;

using BarbieApp.ViewModels;

namespace BarbieApp.Pages
{
    public sealed partial class HomePage : Page
    {
        InterstitialAd MyVideoAd;
        public HomePage()
        {
           // Kiran Adunit data
            var MyAppID = "9nblggh5lnwp";
            var MyAdUnitId = "11670868";

            //test Adunit data
           // var MyAppID = "d25517cb-12d4-4699-8bdc-52040c712cab";
           // var MyAdUnitId = "11389925";


            // instantiate an InterstitialAd
            MyVideoAd = new InterstitialAd();

            // wire up all 4 events, see below for function templates
            MyVideoAd.AdReady += MyVideoAd_AdReady;
            MyVideoAd.ErrorOccurred += MyVideoAd_ErrorOccurred;
            MyVideoAd.Completed += MyVideoAd_Completed;
            MyVideoAd.Cancelled += MyVideoAd_Cancelled;
  

            // pre-fetch an ad 30-60 seconds before you need it
            MyVideoAd.RequestAd(AdType.Video, MyAppID, MyAdUnitId);

            ViewModel = new MainViewModel(12);            
            InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Required;
			commandBar.DataContext = ViewModel;
			searchBox.SearchCommand = SearchCommand;
			this.SizeChanged += OnSizeChanged;
        }		
        public MainViewModel ViewModel { get; set; }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            await this.ViewModel.LoadDataAsync();
			//Page cache requires set commandBar in code
			ShellPage.Current.ShellControl.SetCommandBar(commandBar);
            ShellPage.Current.ShellControl.SelectItem("Home");
        }

		private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            searchBox.SearchWidth = e.NewSize.Width > 640 ? 230 : 190;
        }

		public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand<string>(text =>
                {
                    searchBox.Reset();
                    ShellPage.Current.ShellControl.CloseLeftPane();                    
                    NavigationService.NavigateToPage("SearchPage", text, true);
                },
                SearchViewModel.CanSearch);
            }
        }

        void MyVideoAd_AdReady(object sender, object e)
        {
            // code
            var A = MyVideoAd.State;
            MyVideoAd.Show();

        }

        void MyVideoAd_ErrorOccurred(object sender, AdErrorEventArgs e)
        {
            // code
            var A = MyVideoAd.State;
        }

        void MyVideoAd_Completed(object sender, object e)
        {
            // code
            var A = MyVideoAd.State;
        }

        void MyVideoAd_Cancelled(object sender, object e)
        {
            // code
            var A = MyVideoAd.State;
        }


        private void Hyperlink_Click(Windows.UI.Xaml.Documents.Hyperlink sender, Windows.UI.Xaml.Documents.HyperlinkClickEventArgs args)
        {
            var advertisingId = Windows.System.UserProfile.AdvertisingManager.AdvertisingId;
            sender.NavigateUri = new System.Uri("https://dpa-fwl.microsoft.com/redirect.html?referrerID=" + advertisingId + "&rurl=http://play.barbie.com/en-us");
        }


    }
}
