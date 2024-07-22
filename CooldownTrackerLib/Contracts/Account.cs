using System.Collections.Generic;

namespace CooldownTrackerLib.Contracts
{
    public class Account
    {
        public string Name { get; set; }
        public List<ActivityBase> Activities { get; set; }
    }
}