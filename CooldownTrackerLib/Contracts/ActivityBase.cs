using System;

namespace CooldownTrackerLib.Contracts
{
    public abstract class ActivityBase
    {
        public string Name { get; set; }
        public DateTime EndTime { get; set; }
        public abstract void Update();
    }
}