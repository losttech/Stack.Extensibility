namespace LostTech.Stack.Extensibility.Metadata
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
        /// <summary>
        /// Tells if window paints its borders bypassing system
        /// </summary>
        [XmlAttribute]
        [DataMember]
        [DefaultValue(false)]
        public bool CustomBorders { get; set; }
        /// <summary>
        /// Tells if window renders its chrome (like titlebar) bypassing system
        /// </summary>
        [XmlAttribute]
        [DataMember]
        [DefaultValue(false)]
        public bool CustomChrome { get; set; }
        /// <summary>
        /// Amount of additional space to leave empty around window.
        /// Can be negative (will lead to window expansion beyond target borders).
        /// </summary>
        [DataMember]
        [DefaultValue(null)]
        public Borders Margin { get; set; }
        /// <summary>
        /// Allows expanding window vertically up to a certain amount.
        /// By default, expansion is infinite (INF).
        /// </summary>
        [XmlAttribute]
        [DataMember]
        [DefaultValue(double.PositiveInfinity)]
        public double VerticalExpansion { get; set; } = double.PositiveInfinity;
        /// <summary>
        /// Allows expanding window horizontally up to a certain amount.
        /// By default, expansion is infinite (INF).
        /// </summary>
        [XmlAttribute]
        [DataMember]
        [DefaultValue(double.PositiveInfinity)]
        public double HorizontalExpansion { get; set; } = double.PositiveInfinity;
    }
}
