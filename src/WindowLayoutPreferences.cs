namespace LostTech.Stack.Extensibility
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public sealed class WindowLayoutPreferences
    {
        [DataMember]
        public double Width { get; set; }

        [DataMember]
        public double Height { get; set; }

        [DataMember]
        public double? WidthWeight { get; set; }

        [DataMember]
        public double? HeightWeight { get; set; }

        [DataMember]
        public List<string> PreferredPositions { get; } = new List<string>();

        [DataMember]
        public Borders Margin { get; set; }
    }
}
