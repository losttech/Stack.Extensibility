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
        CommonStringMatchFilter classFilter;
        CommonStringMatchFilter titleFilter;
        CommonStringMatchFilter processFilter;

        public bool Matches(IntPtr windowHandle)
        {
            try
            {
                if (!this.ClassFilter.MatchesAnything()) {
                    string className = User32.GetClassName(windowHandle, maxLength: 4096);
                    if (!this.ClassFilter.Matches(className))
                        return false;
                }

                if (!this.TitleFilter.MatchesAnything()) {
                    string title = User32.GetWindowText(windowHandle);
                    if (!this.TitleFilter.Matches(title))
                        return false;
                }

                if (!this.ProcessFilter.MatchesAnything()) {
                    User32.GetWindowThreadProcessId(windowHandle, out int processID);
                    if (processID != 0) {
                        try {
                            Process process = Process.GetProcessById(processID);
                            if (process.ProcessName == "ApplicationFrameHost") {
                                processID = GetUwpProcessID(windowHandle);
                                if (processID != 0)
                                    process = Process.GetProcessById(processID);
                            }

                            if (!this.ProcessFilter.Matches(process.ProcessName))
                                return false;
                        } catch (ArgumentException) { } catch (InvalidOperationException) { }
                    }
                }
            }
            catch (Win32Exception e)
            {
                Debug.WriteLine($"Can't obtain window class, title or process name: {e}");
                return false;
            }

            return true;
        }

        [DllImport("user32", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool EnumChildWindows(IntPtr hWndParent, User32.WNDENUMPROC lpEnumFunc, IntPtr lParam);

        /// <summary>
        /// Find child process for uwp apps, edge, mail, etc.
        /// </summary>
        static int GetUwpProcessID(IntPtr hostWindowHandle) {
            User32.GetWindowThreadProcessId(hostWindowHandle, out var hostPID);
            if (hostPID == 0)
                return 0;

            int result = 0;
            EnumChildWindows(hostWindowHandle, (child, _) => {
                User32.GetWindowThreadProcessId(child, out var childPID);
                if (childPID != hostPID && childPID != 0) {
                    result = childPID;
                    return false;
                }
                return true;
            }, IntPtr.Zero);

            return result;
        }

        /// <summary>
        /// Filters windows by window class (Win32 only).
        /// </summary>
        [DefaultValue(null)]
        public CommonStringMatchFilter ClassFilter {
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
        public CommonStringMatchFilter TitleFilter {
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
        public CommonStringMatchFilter ProcessFilter {
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
                result.Append($"proc: {this.processFilter.Value}; ");
            if (!this.TitleFilter.MatchesAnything())
                result.Append($"win: {this.titleFilter.Value}; ");
            if (!this.classFilter.MatchesAnything())
                result.Append($"cls: {this.classFilter.Value};");
            return result.ToString();
        }
    }
}
