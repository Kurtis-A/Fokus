using LiveCharts;
using System;

namespace Fokus.ViewModels
{
    public class HomeCalorieBurntViewModel : ViewModelBase
    {
        private DateTime selectedDate;
        private string currentStartDate;
        private string currentEndDate;
        private string previousStartDate;
        private string previousEndDate;
        private ChartValues<double> currentWeekValues;
        private ChartValues<double> previousWeekValues;
        private object summaryPanel;

        public HomeCalorieBurntViewModel()
        {
            SelectedDate = DateTime.Now;
        }

        public DateTime SelectedDate
        {
            get => selectedDate; set
            {
                selectedDate = value;
                OnPropertyChanged();
            }
        }

        public DateTime CurrentStartDate { get; set; }

        public DateTime CurrentEndDate { get; set; }

        public string DisplayCurrentStartDate
        {
            get => currentStartDate; set
            {
                currentStartDate = value;
                OnPropertyChanged();
            }
        }

        public string DisplayCurrentEndDate
        {
            get => currentEndDate; set
            {
                currentEndDate = value;
                OnPropertyChanged();
            }
        }

        public string DisplayPreviousStartDate
        {
            get => previousStartDate; set
            {
                previousStartDate = value;
                OnPropertyChanged();
            }
        }

        public string DisplayPreviousEndDate
        {
            get => previousEndDate; set
            {
                previousEndDate = value;
                OnPropertyChanged();
            }
        }

        public ChartValues<double> CurrentWeekValues
        {
            get => currentWeekValues; set
            {
                currentWeekValues = value;
                OnPropertyChanged();
            }
        }

        public ChartValues<double> PreviousWeekValues
        {
            get => previousWeekValues; set
            {
                previousWeekValues = value;
                OnPropertyChanged();
            }
        }

        public object SummaryPanel
        {
            get => summaryPanel;
            set
            {
                summaryPanel = value;
                OnPropertyChanged();
            }
        }
    }
}