using System.Windows.Input;

namespace GenericCooldownTrackerWpf
{
    public class CooldownActivityViewModel : ActivityBaseViewModel
    {
        public int? MaxAttempts { get; set; }
        public int? CooldownDuration { get; set; }
        public bool UserProvidedDuration { get; set; }
        public bool ResetOnCollection { get; set; }
        public int Attempts { get; set; }
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

        public ICommand SetDurationCommand { get; }

        public CooldownActivityViewModel(string name, int? maxAttempts, int? cooldownDuration, bool userProvidedDuration, bool resetOnCollection, DateTime resetTime)
        {
            Name = name;
            MaxAttempts = maxAttempts;
            CooldownDuration = cooldownDuration;
            UserProvidedDuration = userProvidedDuration;
            ResetOnCollection = resetOnCollection;
            ResetTime = resetTime;
            SetDurationCommand = new RelayCommand(SetDuration);
        }

        private void SetDuration()
        {
            var parts = DurationInput.Split(':');
            if (parts.Length == 3 && int.TryParse(parts[0], out int hours) && int.TryParse(parts[1], out int minutes) && int.TryParse(parts[2], out int seconds))
            {
                int totalSeconds = hours * 3600 + minutes * 60 + seconds;
                EndTime = DateTime.Now.AddSeconds(totalSeconds);
                OnPropertyChanged(nameof(EndTime));
            }
        }
    }
}