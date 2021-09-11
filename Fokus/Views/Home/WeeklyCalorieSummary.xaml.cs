using Fokus.ViewModels;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Fokus.Views
{
    /// <summary>
    /// Interaction logic for WeeklyCalorieSummary.xaml
    /// </summary>
    public partial class WeeklyCalorieSummary : UserControl
    {
        private readonly ObservableCollection<ActivityViewModel> _activityCollection;

        public WeeklyCalorieSummary(ObservableCollection<ActivityViewModel> activityCollection)
        {
            InitializeComponent();
            _activityCollection = activityCollection;
            Load();
            DataContext = this;
        }

        public ObservableCollection<HomeCaloriesSummaryViewModel> SummaryCollection { get; set; }

        private void Load()
        {
            var results = new ConcurrentDictionary<string, int>();
            foreach (var item in _activityCollection)
            {
                results.AddOrUpdate(item.Name, item.Calories, (k, v) => v + item.Calories);
            }

            var collection = new ObservableCollection<HomeCaloriesSummaryViewModel>();

            foreach (var result in results)
            {
                collection.Add(new HomeCaloriesSummaryViewModel() { Activity = result.Key, Calories = result.Value });
            }

            SummaryCollection = collection;
        }
    }
}