namespace LostTech.Stack.Extensibility.Events
{
    using System.Xml.Serialization;

    [XmlInclude(typeof(ScreenEvent))]
    [XmlInclude(typeof(WindowEvent))]
    public interface IAutomationEvent
    {
    }
}
