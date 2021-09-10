using Fokus.Services;
using Fokus.Views;
using System.Collections.ObjectModel;
using System.Windows;

namespace Fokus
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const WindowState minimized = WindowState.Minimized;
        private readonly ActivityService _service;

        public MainWindow(ActivityService service)
        {
            InitializeComponent();
            _service = service;
            Load();
        }

        private async void Load()
        {
            Activities = new ObservableCollection<ActivityViewModel>(await _service.FetchActivities());
            var temp = new Home(_service);
            ContentPresenter.Content = temp;
        }

        public ObservableCollection<ActivityViewModel> Activities { get; set; }

        #region Window Events

        private void CloseApplication(object sender, RoutedEventArgs e) => Application.Current.Shutdown();

        private void MaximiseApplication(object sender, RoutedEventArgs e) => WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;

        private void MinimiseApplication(object sender, RoutedEventArgs e) => WindowState = minimized;

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) => DragMove();

        #endregion Window Events
    }
}