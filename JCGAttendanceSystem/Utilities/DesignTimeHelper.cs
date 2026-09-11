using System;
using System.ComponentModel;
using System.Diagnostics;

namespace JCGAttendanceSystem.Utilities
{
    internal static class DesignTimeHelper
    {
        public static bool IsDesignMode
        {
            get
            {
                if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return true;
                try
                {
                    var process = Process.GetCurrentProcess().ProcessName ?? string.Empty;
                    return process.Equals("devenv", StringComparison.OrdinalIgnoreCase) ||
                           process.IndexOf("DesignToolsServer", StringComparison.OrdinalIgnoreCase) >= 0;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
