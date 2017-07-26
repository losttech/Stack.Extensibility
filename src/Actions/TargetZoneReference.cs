namespace LostTech.Stack.Extensibility.Actions
{
    using System.Xml.Serialization;

    public abstract class TargetZoneReference
    {
    }

    public sealed class NamedZone : TargetZoneReference
    {
        [XmlText]
        public string Name { get; set; }
    }

    public sealed class ZoneWithId : TargetZoneReference
    {
        [XmlText]
        public string Id { get; set; }
    }
}