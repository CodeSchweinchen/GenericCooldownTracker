using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GenericCooldownTrackerWpf
{
    public abstract class ActivityBaseViewModel : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public DateTime EndTime { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}