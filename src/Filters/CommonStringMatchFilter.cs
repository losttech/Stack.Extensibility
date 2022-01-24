namespace LostTech.Stack.Extensibility.Filters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Xml.Serialization;
    using LostTech.App.DataBinding;

    public class CommonStringMatchFilter : StringMatchFilter<string>, ICopyable<CommonStringMatchFilter>
    {
        MatchOption match;
        Regex? regex;

        public override bool Matches(string value)
        {
            if (value == null)
                return false;

            switch (this.match) {
            case MatchOption.Anywhere:
                return value.Contains(this.Value ?? string.Empty);
            case MatchOption.Exact:
                return value == this.Value;
            case MatchOption.Prefix:
                return value.StartsWith(this.Value ?? string.Empty);
            case MatchOption.Suffix:
                return value.EndsWith(this.Value ?? string.Empty);
            case MatchOption.Regex:
                if (this.Value is null) return true;
                this.regex ??= new Regex(this.Value);
                return this.regex.IsMatch(value);
            default:
                return false;
            }
        }

        public CommonStringMatchFilter Copy() => new CommonStringMatchFilter {
            Match = this.Match,
            Value = this.Value,
        };

        public void CopyTo(CommonStringMatchFilter other) {
            if (other is null) throw new ArgumentNullException(nameof(other));

            other.Match = this.Match;
            other.Value = this.Value;
        }

        /// <summary>
        /// Matching mode. Default is <see cref="MatchOption.Anywhere"/>
        /// </summary>
        /// <seealso cref="MatchOption"/>
        [XmlAttribute]
        public MatchOption Match {
            get => this.match;
            set {
                if (value == this.match)
                    return;
                this.match = value;
                this.OnPropertyChanged();
            }
        }

        static readonly MatchOption[] MatchOptionsSingleton = (MatchOption[])Enum.GetValues(typeof(MatchOption));
        public static MatchOption[] MatchOptions => MatchOptionsSingleton.ToArray();

        public enum MatchOption
        {
            Anywhere,
            Exact,
            Prefix,
            Suffix,
            Regex,
        }
    }

    public static class CommonStringMatchFilterExtensions
    {
        public static bool MatchesAnything(this CommonStringMatchFilter? filter) =>
            filter?.Value == null || filter.Value  == "" && filter.Match == CommonStringMatchFilter.MatchOption.Anywhere;
    }
}
