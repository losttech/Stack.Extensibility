using System;
using System.Diagnostics;

using PInvoke;

namespace LostTech.Stack.Extensibility.Filters;

public sealed class Title : CommonStringMatchFilter, IFilter<IntPtr> {
    public bool Matches(IntPtr windowHandle) {
        if (this.MatchesAnything()) return true;

        try {
            string title = User32.GetWindowText(windowHandle);
            return this.Matches(title);
        } catch (Win32Exception e) {
            Debug.WriteLine($"Can't obtain window title: {e}");
            return false;
        }
    }
}
