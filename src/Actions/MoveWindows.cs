namespace LostTech.Stack.Extensibility.Actions
{
    using System.Collections.Generic;
    using LostTech.Stack.Extensibility.Filters;

    public sealed class MoveWindows
    {
        public List<IWindowGroup> Matching { get; } = new List<IWindowGroup>();
        public TargetZoneReference To { get; set; }
    }
}
