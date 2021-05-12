using System;
using System.Drawing;
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

  public enum DeviceCap
  {
    VERTRES = 10,
    DESKTOPVERTRES = 117
  }
  public static float GetScalingFactor()
  {
    Graphics g = Graphics.FromHwnd(IntPtr.Zero);
    IntPtr desktop = g.GetHdc();
    int LogicalScreenHeight = GetDeviceCaps(desktop, (int)DeviceCap.VERTRES);
    int PhysicalScreenHeight = GetDeviceCaps(desktop, (int)DeviceCap.DESKTOPVERTRES);

    float ScreenScalingFactor = (float)PhysicalScreenHeight / (float)LogicalScreenHeight;

    return ScreenScalingFactor; // 1.25 = 125%
  }
}
