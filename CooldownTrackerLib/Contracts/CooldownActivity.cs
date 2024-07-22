using System;

namespace CooldownTrackerLib.Contracts
{
    public class CooldownActivity : ActivityBase
    {
        public int? MaxAttempts { get; set; }
        public int? CooldownDuration { get; set; }
        public bool UserProvidedDuration { get; set; }
        public bool ResetOnCollection { get; set; }
        public int Attempts { get; set; }
        public DateTime ResetTime { get; set; }

        public CooldownActivity(string name, int? maxAttempts, int? cooldownDuration, bool userProvidedDuration, bool resetOnCollection, DateTime resetTime)
        {
            Name = name;
            MaxAttempts = maxAttempts;
            CooldownDuration = cooldownDuration;
            UserProvidedDuration = userProvidedDuration;
            ResetOnCollection = resetOnCollection;
            ResetTime = resetTime;
            Attempts = 0;
        }

        public override void Update()
        {
            if (MaxAttempts.HasValue && Attempts >= MaxAttempts && DateTime.Now >= ResetTime)
            {
                Attempts = 0; // Reset attempts at the reset time
            }
        }

        public void SetDuration(int durationInSeconds)
        {
            if (UserProvidedDuration)
            {
                EndTime = DateTime.Now.AddSeconds(durationInSeconds);
            }
            else if (CooldownDuration.HasValue)
            {
                EndTime = DateTime.Now.AddSeconds(CooldownDuration.Value);
            }
        }

        public void Use()
        {
            if (MaxAttempts.HasValue && Attempts >= MaxAttempts.Value)
                return;

            if (UserProvidedDuration || CooldownDuration.HasValue)
            {
                Attempts++;
                SetDuration(CooldownDuration ?? 0);
            }
        }

        public void Collect()
        {
            if (ResetOnCollection)
            {
                Attempts = 0;
            }
        }
    }
}