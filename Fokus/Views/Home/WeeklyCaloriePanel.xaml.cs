using Fokus.ViewModels;
using LiveCharts;
using System;
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
    /// Interaction logic for WeeklyCaloriePanel.xaml
    /// </summary>
    public partial class WeeklyCaloriePanel : UserControl
    {
        private ObservableCollection<ActivityViewModel> Activity;
        private HomeCalorieBurntViewModel burntViewModel;

        public WeeklyCaloriePanel(ObservableCollection<ActivityViewModel> activitiesCollection)
        {
            InitializeComponent();

            Activity = activitiesCollection;

            burntViewModel = new HomeCalorieBurntViewModel();

            Load(DateTime.Now);
        }

        public object SummaryPanel { get; set; }

        private void Load(DateTime date)
        {
            AxisX.Labels = new List<string>() { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };

            burntViewModel.CurrentWeekValues = new ChartValues<double>();
            burntViewModel.PreviousWeekValues = new ChartValues<double>();

            LoadCurrentWeek(date);
            LoadPreviousWeek(date);
            LoadSummaryPanel();

            myChart.LegendLocation = LegendLocation.Bottom;

            DataContext = burntViewModel;
        }

        private void LoadCurrentWeek(DateTime date)
        {
            var currentWeekDates = GetCurrentWeekDates(date);

            var currentStartDate = currentWeekDates.FirstOrDefault(c => c.Key == "start").Value;
            var currentEndDate = currentWeekDates.FirstOrDefault(c => c.Key == "end").Value;

            burntViewModel.CurrentStartDate = currentStartDate.ToString("dddd, dd MMM y");
            burntViewModel.CurrentEndDate = currentEndDate.ToString("dddd, dd MMM y");

            foreach (var item in Activity.Where(a => a.Created >= currentStartDate && a.Created <= currentEndDate).OrderBy(c => c.Created))
            {
                burntViewModel.CurrentWeekValues.Add(item.Calories);
            }

            myChart.Update();
        }

        private void LoadPreviousWeek(DateTime date)
        {
            var previousWeekDates = GetPreviousWeekDates(date);

            var previousStartDate = previousWeekDates.FirstOrDefault(c => c.Key == "start").Value;
            var previousEndDate = previousWeekDates.FirstOrDefault(c => c.Key == "end").Value;

            burntViewModel.PreviousStartDate = previousStartDate.ToString("dddd, dd MMM y");
            burntViewModel.PreviousEndDate = previousEndDate.ToString("dddd, dd MMM y");

            foreach (var item in Activity.Where(a => a.Created >= previousStartDate && a.Created <= previousEndDate).OrderBy(c => c.Created))
            {
                burntViewModel.PreviousWeekValues.Add(item.Calories);
            }

            myChart.Update();
        }

        private void LoadSummaryPanel()
        {
            var panel = new WeeklyCalorieSummary(Activity);
            burntViewModel.SummaryPanel = panel;
        }

        private Dictionary<string, DateTime> GetCurrentWeekDates(DateTime selectedDate)
        {
            var dates = new Dictionary<string, DateTime>();

            var baseDate = selectedDate;

            var thisWeekStart = baseDate.AddDays(-(int)baseDate.DayOfWeek);
            var thisWeekEnd = thisWeekStart.AddDays(7).AddSeconds(-1);

            dates.Add("start", thisWeekStart);
            dates.Add("end", thisWeekEnd);

            return dates;
        }

        private Dictionary<string, DateTime> GetPreviousWeekDates(DateTime selectedDate)
        {
            var dates = new Dictionary<string, DateTime>();

            var baseDate = selectedDate;

            var thisWeekStart = baseDate.AddDays(-(int)baseDate.DayOfWeek).AddDays(-7);
            var thisWeekEnd = thisWeekStart.AddDays(7).AddSeconds(-1);

            dates.Add("start", thisWeekStart);
            dates.Add("end", thisWeekEnd);

            return dates;
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dateSelector.DisplayDate == null) return;

            if (dateSelector.DisplayDate.ToShortDateString() != burntViewModel.SelectedDate.ToShortDateString())
                Load(DateTime.Parse(sender.ToString()));
        }
    }
}