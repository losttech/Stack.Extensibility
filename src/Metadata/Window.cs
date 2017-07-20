namespace LostTech.Stack.Extensibility.Metadata
{
    using System.Collections.Generic;
    using System.Xml.Serialization;
    using LostTech.Stack.Extensibility.Filters;

    public sealed class Window
    {
        [XmlElement("Filter")]
        public List<WindowFilter> Filters { get; } = new List<WindowFilter>();
        public WindowLayout Layout { get; set; }
    }
}