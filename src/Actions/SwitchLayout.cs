namespace LostTech.Stack.Extensibility.Actions
{
    using System.Xml.Serialization;

    public sealed class SwitchLayout
    {
        // TODO: which screen should switch layout
        [XmlAttribute]
        public string LayoutName { get; set; }
    }
}
