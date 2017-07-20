namespace LostTech.Stack.Extensibility.Metadata
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    public sealed class App
    {
        [XmlElement("Window")]
        public List<Window> Windows { get; } = new List<Window>();
    }
}
