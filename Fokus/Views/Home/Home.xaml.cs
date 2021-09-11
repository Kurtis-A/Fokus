using Fokus.Services;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace Fokus.Views
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        private readonly ActivityService _service;

        private ObservableCollection<ActivityViewModel> Activity;

        public Home(ActivityService service)
        {
            InitializeComponent();

            _service = service;

            LoadAsync();
        }

        public object TopPanel { get; set; }

        private async void LoadAsync()
        {
            Activity = new ObservableCollection<ActivityViewModel>(await _service.FetchActivities());

            var caloriesPanel = new WeeklyCaloriePanel(Activity);
            TopPanel = caloriesPanel;

            DataContext = this;
        }
    }
}