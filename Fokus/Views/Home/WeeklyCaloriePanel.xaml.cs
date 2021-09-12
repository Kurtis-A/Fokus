using Fokus.ViewModels;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;

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

            burntViewModel.SelectedDate = date;

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

            burntViewModel.CurrentStartDate = currentWeekDates.FirstOrDefault(c => c.Key == "start").Value;
            burntViewModel.CurrentEndDate = currentWeekDates.FirstOrDefault(c => c.Key == "end").Value;

            burntViewModel.DisplayCurrentStartDate = burntViewModel.CurrentStartDate.ToString("dddd, dd MMM y");
            burntViewModel.DisplayCurrentEndDate = burntViewModel.CurrentEndDate.ToString("dddd, dd MMM y");

            foreach (var item in Activity.Where(a => a.Created >= burntViewModel.CurrentStartDate && a.Created <= burntViewModel.CurrentEndDate).OrderBy(c => c.Created))
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

            burntViewModel.DisplayPreviousStartDate = previousStartDate.ToString("dddd, dd MMM y");
            burntViewModel.DisplayPreviousEndDate = previousEndDate.ToString("dddd, dd MMM y");

            foreach (var item in Activity.Where(a => a.Created >= previousStartDate && a.Created <= previousEndDate).OrderBy(c => c.Created))
            {
                burntViewModel.PreviousWeekValues.Add(item.Calories);
            }

            myChart.Update();
        }

        private void LoadSummaryPanel()
        {
            burntViewModel.SummaryPanel = null;
            var panel = new WeeklyCalorieSummary(Activity, burntViewModel);
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
            if (dateSelector.DisplayDate == null && sender.ToString() != null) return;

            if (dateSelector.DisplayDate.ToShortDateString() != burntViewModel.SelectedDate.ToShortDateString())
                Load(DateTime.Parse(sender.ToString()));
        }
    }
}