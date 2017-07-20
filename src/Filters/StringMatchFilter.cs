namespace LostTech.Stack.Extensibility.Filters
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Xml.Serialization;
    using LostTech.App.DataBinding;

    public abstract class StringMatchFilter<T> : NotifyPropertyChangedBase, IFilter<T>
    {
        string value;

        /// <summary>
        /// String to search for
        /// </summary>
        [XmlAttribute]
        [DefaultValue(null)]
        public string Value
        {
            get => this.value;
            set {
                if (value == this.value)
                    return;
                this.value = value;
                this.OnPropertyChanged();
            }
        }

        public abstract bool Matches(T value);
    }
}
