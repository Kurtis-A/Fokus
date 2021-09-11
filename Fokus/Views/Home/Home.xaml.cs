using Fokus.Services;
using Fokus.ViewModels;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

            var calorieSummary = new WeeklyCalorieSummary(Activity);

            DataContext = this;
        }
    }
}