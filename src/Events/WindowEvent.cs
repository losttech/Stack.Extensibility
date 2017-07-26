namespace LostTech.Stack.Extensibility.Events
{
    using System.Collections.Generic;
    using System.Xml.Serialization;
    using LostTech.Stack.Extensibility.Filters;

    [XmlInclude(typeof(WindowAppeared))]
    [XmlInclude(typeof(WindowDisappeared))]
    [XmlInclude(typeof(WindowGotFocus))]
    [XmlInclude(typeof(WindowLostFocus))]
    public abstract class WindowEvent
    {
        /// <summary>
        /// List of filters, that tell which windows to match
        /// </summary>
        [XmlElement("Filter")]
        public List<WindowFilter> Filters { get; } = new List<WindowFilter>();
    }

    public class WindowAppeared: WindowEvent { }
    public class WindowDisappeared: WindowEvent { }
    public class WindowGotFocus: WindowEvent { }
    public class WindowLostFocus: WindowEvent { }
}
