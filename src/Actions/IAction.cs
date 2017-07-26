namespace LostTech.Stack.Extensibility.Actions
{
    using System.Xml.Serialization;

    [XmlInclude(typeof(AutoArrange))]
    [XmlInclude(typeof(MoveWindows))]
    [XmlInclude(typeof(SwitchLayout))]
    [XmlInclude(typeof(Yield))]
    public interface IAction
    {
    }
}
