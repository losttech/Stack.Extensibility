namespace LostTech.Stack.Extensibility.Filters
{
    using System.Xml.Serialization;

    public sealed class NamedGroup: IWindowGroup
    {
        [XmlText]
        public string Name { get; set; }
    }
}
