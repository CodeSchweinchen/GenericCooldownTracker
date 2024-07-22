namespace GenericCooldownTrackerWpf
{
    public class DailyActivityViewModel : ActivityBaseViewModel
    {
        public int MaxAttempts { get; set; }
        public int CurrentAttempts { get; set; }
        public DateTime ResetTime { get; set; }

        public DailyActivityViewModel(string name, int maxAttempts, DateTime resetTime)
        {
            Name = name;
            MaxAttempts = maxAttempts;
            ResetTime = resetTime;
        }
    }
}