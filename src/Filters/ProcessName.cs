using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

using PInvoke;

namespace LostTech.Stack.Extensibility.Filters;

public sealed class ProcessName : CommonStringMatchFilter, IFilter<IntPtr> {
    public bool Matches(IntPtr windowHandle) {
        if (this.MatchesAnything()) return true;

        try {
            User32.GetWindowThreadProcessId(windowHandle, out int processID);
            if (processID != 0) {
                try {
                    var process = Process.GetProcessById(processID);
                    if (process is null) return false;
                    string? name = GetName(process);
                    if (name is null) return false;
                    if (name.EndsWith("\\ApplicationFrameHost.exe", StringComparison.InvariantCultureIgnoreCase)) {
                        processID = GetUwpProcessID(windowHandle);
                        if (processID != 0) {
                            process = Process.GetProcessById(processID);
                            name = GetName(process);
                            if (name is null) return false;
                        }
                    }

                    if (!this.Matches(name))
                        return false;
                } catch (ArgumentException) { } catch (InvalidOperationException) { }
            }

            return true;
        } catch (Win32Exception e) {
            Debug.WriteLine($"Can't obtain process name: {e}");
            return false;
        }
    }

    static string? GetName(Process process) {
        try {
            using var handle = new Kernel32.SafeObjectHandle(process.Handle, ownsHandle: false);
            return Kernel32.QueryFullProcessImageName(handle, dwFlags: default);
        } catch (System.ComponentModel.Win32Exception e)
            when (e.NativeErrorCode == (int)Win32ErrorCode.ERROR_ACCESS_DENIED) {
            return null;
        }
    }

    /// <summary>
    /// Find child process for uwp apps, edge, mail, etc.
    /// </summary>
    static int GetUwpProcessID(IntPtr hostWindowHandle) {
        User32.GetWindowThreadProcessId(hostWindowHandle, out int hostPID);
        if (hostPID == 0)
            return 0;

        int result = 0;
        User32.WNDENUMPROC childHandler = (child, _) => {
            User32.GetWindowThreadProcessId(child, out int childPID);
            if (childPID != hostPID && childPID != 0) {
                result = childPID;
                return false;
            }
            return true;
        };
        EnumChildWindows(hostWindowHandle, childHandler, IntPtr.Zero);
        GC.KeepAlive(childHandler);

        return result;
    }

    [DllImport("user32", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool EnumChildWindows(IntPtr hWndParent, User32.WNDENUMPROC lpEnumFunc, IntPtr lParam);
}
