namespace LostTech.Stack.Extensibility.Automation
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    public class Automata
    {
        [XmlElement("Rule")]
        public List<AutomationRule> Rules { get; } = new List<AutomationRule>();
    }
}
