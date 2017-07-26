namespace LostTech.Stack.Extensibility.Events
{
    using System.Xml.Serialization;

    [XmlInclude(typeof(ScreenAttached))]
    [XmlInclude(typeof(ScreenDetached))]
    public abstract class ScreenEvent
    {
    }

    public class ScreenAttached: ScreenEvent { }
    public class ScreenDetached: ScreenEvent { }
}
