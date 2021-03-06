﻿namespace LostTech.Stack.Extensibility.Filters
{
    using System.Xml.Serialization;

    [XmlInclude(typeof(CommonStringMatchFilter))]
    public interface IFilter<in T>
    {
        bool Matches(T value);
    }
}
