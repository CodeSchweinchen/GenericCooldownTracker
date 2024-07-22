using System;

namespace CooldownTrackerLib.Contracts
{
    public class DailyActivity : ActivityBase
    {
        public int MaxAttempts { get; set; }
        public int CurrentAttempts { get; set; }
        public DateTime ResetTime { get; set; }

        public DailyActivity(string name, int maxAttempts, DateTime resetTime)
        {
            Name = name;
            MaxAttempts = maxAttempts;
            ResetTime = resetTime;
            CurrentAttempts = 0;
        }

        public override void Update()
        {
            if (CurrentAttempts >= MaxAttempts && DateTime.Now >= ResetTime)
            {
                CurrentAttempts = 0; // Reset completions at the reset time
            }
        }

        public void Complete()
        {
            if (CurrentAttempts < MaxAttempts)
            {
                CurrentAttempts++;
            }
        }
    }
}