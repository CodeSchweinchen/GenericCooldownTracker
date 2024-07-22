namespace CooldownTrackerLib.Contracts
{
    public abstract class Cooldown
    {
        public string Name { get; set; }
        public abstract void Update();
    }
}
