using System;
using System.Windows.Input;

using Windows.ApplicationModel;
using Windows.UI.Xaml.Media.Imaging;

using AppStudio.Uwp;
using AppStudio.Uwp.Commands;

namespace BarbieApp.ViewModels
{
    public class AboutThisAppViewModel : PageViewModelBase
    {
		public AboutThisAppViewModel()
        {
            this.AppName = "Barbie App";
            this.Title = "NavigationPaneAbout".StringResource();
            this.Publisher = "Ronav Apps";
            this.AppVersion = string.Format("{0}.{1}.{2}.{3}", Package.Current.Id.Version.Major, Package.Current.Id.Version.Minor, Package.Current.Id.Version.Build, Package.Current.Id.Version.Revision);
            this.AboutText = "Barbie: Life in the Dreamhouse is an online web series of computer-animated short" +
    "s produced by Arc Productions and distributed by Mattel. The series debuted in M" +
    "ay 11, 2012 and is available on Barbie.com, Netflix and YouTube.";
            this.AppLogo = new BitmapImage(new Uri("ms-appx:///Assets/ApplicationLogo.png"));
            this.Privacy = "http://1drv.ms/1jzSlV5";
            this.WasLibs = "https://github.com/wasteam/waslibs";
            this.WindowsAppStudioWeb = "https://appstudio.windows.com/";
            this.NewtonsoftWeb = "http://www.newtonsoft.com/json";
        }

		public string AppName { get; set; }
        public string Publisher { get; set; }
        public string AppVersion { get; set; }
        public string AboutText { get; set; }
        public string Privacy { get; set; }
        public string WasLibs { get; set; }
        public string WindowsAppStudioWeb { get; set; }
        public string NewtonsoftWeb { get; set; }
        public BitmapImage AppLogo { get; set; }

		private bool _isMoreInfoVisible;
        public bool IsMoreInfoVisible
        {
            get { return _isMoreInfoVisible; }
            set { SetProperty(ref _isMoreInfoVisible, value); }
        }

        private ICommand _viewMoreInfoCommand;
        public ICommand ViewMoreInfoCommand
        {
            get
            {
                if (_viewMoreInfoCommand == null)
                {
                    _viewMoreInfoCommand = new RelayCommand(() => { IsMoreInfoVisible = !IsMoreInfoVisible; });
                }
                return _viewMoreInfoCommand;
            }
        }
    }
}

