using System;
using System.Diagnostics;

using PInvoke;

namespace LostTech.Stack.Extensibility.Filters;

public sealed class Win32Class : CommonStringMatchFilter, IFilter<IntPtr> {
    public bool Matches(IntPtr windowHandle) {
        if (this.MatchesAnything()) return true;

        try {
            string className = User32.GetClassName(windowHandle, maxLength: 4096);
            return this.Matches(className);
        } catch (Win32Exception e) {
            Debug.WriteLine($"Can't obtain window class: {e}");
            return false;
        }
    }
}
