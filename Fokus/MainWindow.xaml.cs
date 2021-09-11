using Fokus.Services;
using Fokus.Views;
using System;
using System.Windows;

namespace Fokus
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ActivityService _activityService;

        public MainWindow(ActivityService activityService)
        {
            InitializeComponent();

            _activityService = activityService;

            Load();
        }

        private void Load()
        {
            var home = new Home(_activityService);
            ContentPresenter.Content = home;
            DataContext = this;
        }

        public string Date { get; set; } = DateTime.Now.ToString("dddd dd MMM yy");

        #region Window Events

        private void CloseApplication(object sender, RoutedEventArgs e) => Application.Current.Shutdown();

        private void MaximiseApplication(object sender, RoutedEventArgs e) => WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;

        private void MinimiseApplication(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) => DragMove();

        #endregion Window Events
    }
}