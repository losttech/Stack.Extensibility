namespace LostTech.Stack.Extensibility
{
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [DataContract]
    public sealed class Borders
    {
        [XmlAttribute]
        [DataMember]
        public double Top { get; set; }
        [XmlAttribute]
        [DataMember]
        public double Bottom { get; set; }
        [XmlAttribute]
        [DataMember]
        public double Left { get; set; }
        [XmlAttribute]
        [DataMember]
        public double Right { get; set; }
    }
}
