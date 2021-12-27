namespace Macabresoft.Core;

using System.Diagnostics;
using System.Runtime.InteropServices;

/// <summary>
/// A helper class for web operations.
/// </summary>
public static class WebHelper {
    /// <summary>
    /// Opens a URL in the default web browser. This does not do any URL verification, so only call it for URLs you know to exist.
    /// </summary>
    /// <param name="url">The URL to open.</param>
    public static void OpenInBrowser(string url) {
        try {
            Process.Start(url);
        }
        catch {
            // hack because of this: https://github.com/dotnet/corefx/issues/10361
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                url = url.Replace("&", "^&");
                Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) {
                Process.Start("xdg-open", url);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) {
                Process.Start("open", url);
            }
            else {
                throw;
            }
        }
    }
}