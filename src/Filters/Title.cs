using System;
using System.Buffers;
using System.Diagnostics;

using PInvoke;

namespace LostTech.Stack.Extensibility.Filters;

public sealed class Title : CommonStringMatchFilter, IFilter<IntPtr> {
    public bool Matches(IntPtr windowHandle) {
        if (this.MatchesAnything()) return true;

        try {
            string? title = GetWindowText(windowHandle);
            if (title is null) return false;
            return this.Matches(title);
        } catch (Win32Exception e) {
            Debug.WriteLine($"Can't obtain window title: {e}");
            return false;
        }
    }

    static string? GetWindowText(IntPtr hwnd) {
        while (true) {
            int maxLen = User32.GetWindowTextLength(hwnd);
            if (maxLen == 0) {
                var error = Kernel32.GetLastError();
                return error switch {
                    Win32ErrorCode.ERROR_SUCCESS => string.Empty,
                    Win32ErrorCode.ERROR_INVALID_WINDOW_HANDLE => null,
                    _ => throw new Win32Exception(error),
                };
            }


            char[] buffer = ArrayPool<char>.Shared.Rent(maxLen + 2);
            try {
                int finalLength = User32.GetWindowText(hwnd, buffer, maxLen + 1);
                
                if (finalLength == 0) {
                    var error = Kernel32.GetLastError();
                    return error switch {
                        Win32ErrorCode.ERROR_SUCCESS => string.Empty,
                        Win32ErrorCode.ERROR_INVALID_WINDOW_HANDLE => null,
                        _ => throw new Win32Exception(error),
                    };
                }
                
                if (finalLength > maxLen)
                    continue;

                return new string(buffer, 0, finalLength);
            } finally {
                ArrayPool<char>.Shared.Return(buffer);
            }
        }
    }
}
