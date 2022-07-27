using System;
using System.Buffers;
using System.Diagnostics;

using PInvoke;

using static PInvoke.Win32ErrorCode;

namespace LostTech.Stack.Extensibility.Filters;

public sealed class Win32Class : CommonStringMatchFilter, IFilter<IntPtr> {
    public bool Matches(IntPtr windowHandle) {
        if (this.MatchesAnything()) return true;

        try {
            string? className = GetClassName(windowHandle);
            if (className is null) return false;
            return this.Matches(className);
        } catch (Win32Exception e) {
            Debug.WriteLine($"Can't obtain window class: {e}");
            return false;
        }
    }

    static string? GetClassName(IntPtr hwnd) {
        int maxLen = 256;
        while (true) {
            char[] buffer = ArrayPool<char>.Shared.Rent(maxLen + 2);
            try {
                int finalLength = User32.GetClassName(hwnd, buffer, maxLen + 1);

                if (finalLength == 0) {
                    var error = Kernel32.GetLastError();
                    return error switch {
                        ERROR_SUCCESS => string.Empty,
                        ERROR_INVALID_WINDOW_HANDLE => null,
                        _ => throw new PInvoke.Win32Exception(error),
                    };
                }

                if (finalLength > maxLen) {
                    maxLen = checked(maxLen * 2);
                    continue;
                }

                return new string(buffer, 0, finalLength);
            } finally {
                ArrayPool<char>.Shared.Return(buffer);
            }
        }
    }
}
