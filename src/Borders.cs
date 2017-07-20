namespace LostTech.Stack.Extensibility
{
    using System.ComponentModel;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [DataContract]
    public sealed class Borders
    {
        [XmlAttribute]
        [DataMember]
        [DefaultValue(0d)]
        public double Top { get; set; }
        [XmlAttribute]
        [DataMember]
        [DefaultValue(0d)]
        public double Bottom { get; set; }
        [XmlAttribute]
        [DataMember]
        [DefaultValue(0d)]
        public double Left { get; set; }
        [XmlAttribute]
        [DataMember]
        [DefaultValue(0d)]
        public double Right { get; set; }
    }
}
