using LiveCharts;
using System;
using System.Collections.Generic;
using System.Text;

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

        public string CurrentStartDate
        {
            get => currentStartDate; set
            {
                currentStartDate = value;
                OnPropertyChanged();
            }
        }

        public string CurrentEndDate
        {
            get => currentEndDate; set
            {
                currentEndDate = value;
                OnPropertyChanged();
            }
        }

        public string PreviousStartDate
        {
            get => previousStartDate; set
            {
                previousStartDate = value;
                OnPropertyChanged();
            }
        }

        public string PreviousEndDate
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
    }
}