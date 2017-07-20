﻿namespace LostTech.Stack.Extensibility.Metadata
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [DataContract]
    public sealed class WindowLayout
    {
        [XmlAttribute]
        [DataMember]
        [DefaultValue(0d)]
        public double MinHeight { get; set; }
        [XmlAttribute]
        [DataMember]
        [DefaultValue(0d)]
        public double MinWidth { get; set; }
        [XmlAttribute]
        [DataMember]
        [DefaultValue(false)]
        public bool CustomBorders { get; set; }
        [XmlAttribute]
        [DataMember]
        [DefaultValue(false)]
        public bool CustomChrome { get; set; }
        [DataMember]
        [DefaultValue(null)]
        public Borders Margin { get; set; }
        [XmlAttribute]
        [DataMember]
        [DefaultValue(double.PositiveInfinity)]
        public double VerticalExpansion { get; set; } = double.PositiveInfinity;
        [XmlAttribute]
        [DataMember]
        [DefaultValue(double.PositiveInfinity)]
        public double HorizontalExpansion { get; set; } = double.PositiveInfinity;
    }
}
