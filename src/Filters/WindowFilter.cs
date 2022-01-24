namespace LostTech.Stack.Extensibility.Filters
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Text;
    using LostTech.App.DataBinding;
    using PInvoke;
    using Win32Exception = System.ComponentModel.Win32Exception;

    public sealed class WindowFilter : NotifyPropertyChangedBase, IFilter<IntPtr>, ICopyable<WindowFilter>, IWindowGroup
    {
        CommonStringMatchFilter? classFilter;
        CommonStringMatchFilter? titleFilter;
        CommonStringMatchFilter? processFilter;

        public bool Matches(IntPtr windowHandle)
        {
            if (this.ClassFilter is not null) {
                if (!new Win32Class().From(this.ClassFilter).Matches(windowHandle))
                    return false;
            }

            if (this.TitleFilter is not null) {
                if (!new Title().From(this.TitleFilter).Matches(windowHandle))
                    return false;
            }

            if (this.ProcessFilter is not null) {
                if (!new ProcessName().From(this.ProcessFilter).Matches(windowHandle))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Filters windows by window class (Win32 only).
        /// </summary>
        [DefaultValue(null)]
        public CommonStringMatchFilter? ClassFilter {
            get => this.classFilter;
            set {
                if (Equals(value, this.classFilter))
                    return;
                this.classFilter = value;
                this.OnPropertyChanged();
            }
        }
        /// <summary>
        /// Filters windows by their title.
        /// </summary>
        [DefaultValue(null)]
        public CommonStringMatchFilter? TitleFilter {
            get => this.titleFilter;
            set {
                if (Equals(value, this.titleFilter))
                    return;
                this.titleFilter = value;
                this.OnPropertyChanged();
            }
        }
        /// <summary>
        /// Filters windows by their process name.
        /// </summary>
        [DefaultValue(null)]
        public CommonStringMatchFilter? ProcessFilter {
            get => this.processFilter;
            set
            {
                if (Equals(value, this.processFilter))
                    return;

                this.processFilter = value;
                this.OnPropertyChanged();
            }
        }

        public WindowFilter Copy() => new WindowFilter {
            ClassFilter = CopyableExtensions.Copy(this.ClassFilter),
            TitleFilter = CopyableExtensions.Copy(this.TitleFilter),
            ProcessFilter = CopyableExtensions.Copy(this.ProcessFilter),
        };

        public override string ToString() {
            var result = new StringBuilder();
            if (!this.processFilter.MatchesAnything())
                result.Append($"proc: {this.processFilter!.Value}; ");
            if (!this.TitleFilter.MatchesAnything())
                result.Append($"win: {this.titleFilter!.Value}; ");
            if (!this.classFilter.MatchesAnything())
                result.Append($"cls: {this.classFilter!.Value};");
            return result.ToString();
        }
    }
}
