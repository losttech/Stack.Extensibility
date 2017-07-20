namespace LostTech.Stack.Extensibility.Metadata
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;
    using LostTech.Stack.Extensibility.Filters;

    public sealed class Window
    {
        /// <summary>
        /// List of filters, that tell how to find that window
        /// </summary>
        [XmlElement("Filter")]
        public List<WindowFilter> Filters { get; } = new List<WindowFilter>();
        /// <summary>
        /// Layout metadata
        /// </summary>
        public WindowLayout Layout { get; set; }
        /// <summary>
        /// Categories this window belongs to. E.g. IM, Office, etc.
        /// </summary>
        [DataMember]
        [XmlElement("Category")]
        public List<string> Categories { get; } = new List<string>();
        /// <summary>
        /// Behavioral role of this window. E.g. Main, Popup, Informational, etc.
        /// </summary>
        [DataMember]
        [XmlElement("Role")]
        public List<string> Roles { get; } = new List<string>();
    }
}