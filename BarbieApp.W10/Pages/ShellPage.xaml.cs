using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Media.Imaging;

using AppStudio.Uwp;
using AppStudio.Uwp.Controls;
using AppStudio.Uwp.Navigation;

using BarbieApp.Navigation;

namespace BarbieApp.Pages
{
    public sealed partial class ShellPage : Page
    {
        public static ShellPage Current { get; private set; }

        public ShellControl ShellControl
        {
            get { return shell; }
        }

        public Frame AppFrame
        {
            get { return frame; }
        }

        public ShellPage()
        {
            InitializeComponent();

            this.DataContext = this;
            ShellPage.Current = this;

            this.SizeChanged += OnSizeChanged;
            if (SystemNavigationManager.GetForCurrentView() != null)
            {
                SystemNavigationManager.GetForCurrentView().BackRequested += ((sender, e) =>
                {
                    if (SupportFullScreen && ShellControl.IsFullScreen)
                    {
                        e.Handled = true;
                        ShellControl.ExitFullScreen();
                    }
                    else if (NavigationService.CanGoBack())
                    {
                        NavigationService.GoBack();
                        e.Handled = true;
                    }
                });
				
                NavigationService.Navigated += ((sender, e) =>
                {
                    if (NavigationService.CanGoBack())
                    {
                        SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
                    }
                    else
                    {
                        SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
                    }
                });
            }
        }

		public bool SupportFullScreen { get; set; }

		#region NavigationItems
        public ObservableCollection<NavigationItem> NavigationItems
        {
            get { return (ObservableCollection<NavigationItem>)GetValue(NavigationItemsProperty); }
            set { SetValue(NavigationItemsProperty, value); }
        }

        public static readonly DependencyProperty NavigationItemsProperty = DependencyProperty.Register("NavigationItems", typeof(ObservableCollection<NavigationItem>), typeof(ShellPage), new PropertyMetadata(new ObservableCollection<NavigationItem>()));
        #endregion

		protected override void OnNavigatedTo(NavigationEventArgs e)
        {
#if DEBUG
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size { Width = 320, Height = 500 });
#endif
            NavigationService.Initialize(typeof(ShellPage), AppFrame);
			NavigationService.NavigateToPage<HomePage>(e);

            InitializeNavigationItems();

            Bootstrap.Init();
        }		        
		
		#region Navigation
        private void InitializeNavigationItems()
        {
            NavigationItems.Add(AppNavigation.NodeFromAction(
				"Home",
                "Home",
                (ni) => NavigationService.NavigateToRoot(),
                AppNavigation.IconFromSymbol(Symbol.Home)));
            NavigationItems.Add(AppNavigation.NodeFromAction(
				"b15c9d18-5de5-4a4f-ad11-a16c7a3ebc91",
                "Season 1",                
                AppNavigation.ActionFromPage("Season1ListPage"),
				AppNavigation.IconFromGlyph("\ue173")));

            NavigationItems.Add(AppNavigation.NodeFromAction(
				"78f8c5fe-5354-4c71-a62d-4d6e937f551f",
                "Season 2",                
                AppNavigation.ActionFromPage("Season2ListPage"),
				AppNavigation.IconFromGlyph("\ue173")));

            NavigationItems.Add(AppNavigation.NodeFromAction(
				"4c8431d5-2ff7-4b1b-b993-1488fab8d565",
                "Season 3",                
                AppNavigation.ActionFromPage("Season3ListPage"),
				AppNavigation.IconFromGlyph("\ue173")));

            NavigationItems.Add(AppNavigation.NodeFromAction(
				"e74a9b24-c218-40e1-897c-0a432ada6da5",
                "Season 4",                
                AppNavigation.ActionFromPage("Season4ListPage"),
				AppNavigation.IconFromGlyph("\ue173")));

            NavigationItems.Add(AppNavigation.NodeFromAction(
				"881416ff-c375-4fdf-8592-87af00afe85c",
                "Season 5",                
                AppNavigation.ActionFromPage("Season5ListPage"),
				AppNavigation.IconFromGlyph("\ue173")));

            NavigationItems.Add(AppNavigation.NodeFromAction(
				"bd2f29ad-be9a-452d-85a7-3ed7cce9b28c",
                "Season 6",                
                AppNavigation.ActionFromPage("Season6ListPage"),
				AppNavigation.IconFromGlyph("\ue173")));

            NavigationItems.Add(AppNavigation.NodeFromAction(
				"dbe9c15a-9a43-4e31-afd0-7f228e4342c1",
                "Season 7",                
                AppNavigation.ActionFromPage("Season7ListPage"),
				AppNavigation.IconFromGlyph("\ue173")));

            NavigationItems.Add(NavigationItem.Separator);

            NavigationItems.Add(AppNavigation.NodeFromControl(
				"About",
                "NavigationPaneAbout".StringResource(),
                new AboutPage(),
                AppNavigation.IconFromImage(new Uri("ms-appx:///Assets/about.png"))));
        }        
        #endregion        

		private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateDisplayMode(e.NewSize.Width);
        }

        private void UpdateDisplayMode(double? width = null)
        {
            if (width == null)
            {
                width = Window.Current.Bounds.Width;
            }
            this.ShellControl.DisplayMode = width > 640 ? SplitViewDisplayMode.CompactOverlay : SplitViewDisplayMode.Overlay;
            this.ShellControl.CommandBarVerticalAlignment = width > 640 ? VerticalAlignment.Top : VerticalAlignment.Bottom;
        }

        private async void OnKeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.F11)
            {
                if (SupportFullScreen)
                {
                    await ShellControl.TryEnterFullScreenAsync();
                }
            }
            else if (e.Key == Windows.System.VirtualKey.Escape)
            {
                if (SupportFullScreen && ShellControl.IsFullScreen)
                {
                    ShellControl.ExitFullScreen();
                }
                else
                {
                    NavigationService.GoBack();
                }
            }
        }
    }
}
