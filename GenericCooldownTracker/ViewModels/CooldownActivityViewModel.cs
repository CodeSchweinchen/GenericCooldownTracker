using System;
using System.Windows.Input;

namespace GenericCooldownTrackerWpf
{
    public class CooldownActivityViewModel : ActivityBaseViewModel
    {
        public int? MaxAttempts { get; set; }
        public int Attempts { get; set; }
        public int? CooldownDuration { get; set; }
        public bool UserProvidedDuration { get; set; }
        public bool ResetOnCollection { get; set; }
        public DateTime ResetTime { get; set; }

        private string _durationInput;
        public string DurationInput
        {
            get => _durationInput;
            set
            {
                _durationInput = value;
                OnPropertyChanged();
            }
        }

        // Neue Properties für Tage, Stunden und Minuten
        private int _daysInput;
        public int DaysInput
        {
            get => _daysInput;
            set
            {
                _daysInput = value;
                OnPropertyChanged();
            }
        }

        private int _hoursInput;
        public int HoursInput
        {
            get => _hoursInput;
            set
            {
                _hoursInput = value;
                OnPropertyChanged();
            }
        }

        private int _minutesInput;
        public int MinutesInput
        {
            get => _minutesInput;
            set
            {
                _minutesInput = value;
                OnPropertyChanged();
            }
        }

        public ICommand SetDurationCommand { get; }

        public CooldownActivityViewModel(string name, int? maxAttempts, int? cooldownDuration, bool userProvidedDuration, bool resetOnCollection, DateTime resetTime)
        {
            Name = name;
            MaxAttempts = maxAttempts;
            CooldownDuration = cooldownDuration;
            UserProvidedDuration = userProvidedDuration;
            ResetOnCollection = resetOnCollection;
            ResetTime = resetTime;
            Attempts = MaxAttempts ?? 0;
            SetDurationCommand = new RelayCommand(SetDuration);
        }

        private void SetDuration()
        {
            // Anpassung der Methode, um die neuen Properties zu nutzen
            int totalSeconds = DaysInput * 86400 + HoursInput * 3600 + MinutesInput * 60;
            EndTime = DateTime.Now.AddSeconds(totalSeconds);
            Attempts = (Attempts > 0) ? Attempts - 1 : 0;
            OnPropertyChanged(nameof(EndTime));
            OnPropertyChanged(nameof(Attempts));
        }
    }
}
