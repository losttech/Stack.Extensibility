namespace LostTech.Stack.Extensibility.Automation
{
    using System.Collections.Generic;
    using LostTech.Stack.Extensibility.Actions;
    using LostTech.Stack.Extensibility.Events;

    public sealed class AutomationRule
    {
        public List<IAutomationEvent> Triggers { get; } = new List<IAutomationEvent>();
        public List<IAction> Actions { get; } = new List<IAction>();
    }
}
