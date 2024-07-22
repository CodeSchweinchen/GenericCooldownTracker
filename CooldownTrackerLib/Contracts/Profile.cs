using System.Collections.Generic;

namespace CooldownTrackerLib.Contracts
{
    public class Profile
    {
        public string Name { get; set; }
        public string ResetTime { get; set; }
        public List<CooldownActivity> Cooldowns { get; set; }
        public List<DailyActivity> DailyActivities { get; set; }
    }
}