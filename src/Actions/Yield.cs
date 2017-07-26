namespace LostTech.Stack.Extensibility.Actions
{
    using System;
    using System.ComponentModel;
    using System.Xml.Serialization;

    public class Yield
    {
        [DefaultValue(null)]
        [XmlAttribute]
        public TimeSpan? Delay { get; set; }
    }
}
