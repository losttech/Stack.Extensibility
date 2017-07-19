namespace LostTech.Stack.Extensibility
{
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [DataContract]
    public sealed class WindowLayout
    {
        [XmlAttribute]
        [DataMember]
        public double MinHeight { get; set; }
        [XmlAttribute]
        [DataMember]
        public double MinWidth { get; set; }
        [XmlAttribute]
        [DataMember]
        public bool CustomBorders { get; set; }
        [XmlAttribute]
        [DataMember]
        public bool CustomChrome { get; set; }
        [DataMember]
        public Borders Margin { get; set; }
    }
}
