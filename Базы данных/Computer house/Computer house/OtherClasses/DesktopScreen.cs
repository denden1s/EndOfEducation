using System;
using System.Runtime.InteropServices;

public static class DesktopScreen
{
    [DllImport("user32.dll")]
    private static extern IntPtr GetDC(IntPtr hWnd);

    [DllImport("gdi32.dll")]
    private static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

    private const int DESKTOPHORZRES = 118;
    private const int DESKTOPVERTRES = 117;

    static DesktopScreen()
    {
        IntPtr hdc = GetDC(IntPtr.Zero);
        Width = GetDeviceCaps(hdc, DESKTOPHORZRES);
        Height = GetDeviceCaps(hdc, DESKTOPVERTRES);
    }

    public static int Width { get; private set; } = GetDeviceCaps(GetDC(IntPtr.Zero), DESKTOPHORZRES);
  public static int Height { get; private set; } = GetDeviceCaps(GetDC(IntPtr.Zero), DESKTOPVERTRES);
}
