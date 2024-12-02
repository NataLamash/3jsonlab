using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3jsonlab
{
    public class Section : INotifyPropertyChanged
    {
        private string id;
        private string name;
        private string coach;
        private string location;
        private List<Schedule> schedule;

        public string Id
        {
            get => id;
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public string Coach
        {
            get => coach;
            set
            {
                if (coach != value)
                {
                    coach = value;
                    OnPropertyChanged(nameof(Coach));
                }
            }
        }

        public string Location
        {
            get => location;
            set
            {
                if (location != value)
                {
                    location = value;
                    OnPropertyChanged(nameof(Location));
                }
            }
        }

        public List<Schedule> Schedule
        {
            get => schedule;
            set
            {
                if (schedule != value)
                {
                    schedule = value;
                    OnPropertyChanged(nameof(Schedule));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
