using Fokus.Services;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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

            dateSelector.SelectedDate = DateTime.Now;
            LoadAsync(DateTime.Now);
        }

        public string CurrentStartDate { get; set; }

        public string CurrentEndDate { get; set; }

        public string PreviousStartDate { get; set; }

        public string PreviousEndDate { get; set; }

        public ChartValues<double> CurrentWeekValues { get; set; }

        public ChartValues<double> PreviousWeekValues { get; set; }

        private async void LoadAsync(DateTime date)
        {
            Activity = new ObservableCollection<ActivityViewModel>(await _service.FetchActivities());

            CurrentWeekValues = new ChartValues<double>();
            PreviousWeekValues = new ChartValues<double>();

            AxisX.Labels = new List<string>() { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };

            LoadCurrentWeek(date);
            LoadPreviousWeek(date);

            DataContext = this;
        }

        private void LoadCurrentWeek(DateTime date)
        {
            var currentWeekDates = GetCurrentWeekDates(date);

            var currentStartDate = currentWeekDates.FirstOrDefault(c => c.Key == "start").Value;
            var currentEndDate = currentWeekDates.FirstOrDefault(c => c.Key == "end").Value;

            CurrentStartDate = currentStartDate.ToString("dddd, dd MMM y");
            CurrentEndDate = currentEndDate.ToString("dddd, dd MMM y");

            foreach (var item in Activity.Where(a => a.Created >= currentStartDate && a.Created <= currentEndDate).OrderBy(c => c.Created))
            {
                CurrentWeekValues.Add(item.Calories);
            }
        }

        private void LoadPreviousWeek(DateTime date)
        {
            var previousWeekDates = GetPreviousWeekDates(date);

            var previousStartDate = previousWeekDates.FirstOrDefault(c => c.Key == "start").Value;
            var previousEndDate = previousWeekDates.FirstOrDefault(c => c.Key == "end").Value;

            PreviousStartDate = previousStartDate.ToString("dddd, dd MMM y");
            PreviousEndDate = previousEndDate.ToString("dddd, dd MMM y");

            foreach (var item in Activity.Where(a => a.Created >= previousStartDate && a.Created <= previousEndDate).OrderBy(c => c.Created))
            {
                PreviousWeekValues.Add(item.Calories);
            }
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
            //if (sender != null)
            //LoadAsync(DateTime.Parse(sender.ToString()));
        }
    }
}