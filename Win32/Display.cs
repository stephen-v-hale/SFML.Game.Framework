using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

using SFML.Graphics;

namespace SFML.Game.Framework.Win32;

public static class DisplayHelper
{
    private const int ENUM_CURRENT_SETTINGS = -1;

    [StructLayout( LayoutKind.Sequential, CharSet = CharSet.Auto )]
    private struct DEVMODE
    {
        private const int CCHDEVICENAME = 32;
        private const int CCHFORMNAME = 32;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHDEVICENAME)]
        public string dmDeviceName;
        public short dmSpecVersion;
        public short dmDriverVersion;
        public short dmSize;
        public short dmDriverExtra;
        public int dmFields;
        public int dmPositionX;
        public int dmPositionY;
        public int dmDisplayOrientation;
        public int dmDisplayFixedOutput;
        public short dmColor;
        public short dmDuplex;
        public short dmYResolution;
        public short dmTTOption;
        public short dmCollate;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHFORMNAME)]
        public string dmFormName;
        public short dmLogPixels;
        public int dmBitsPerPel;
        public int dmPelsWidth;
        public int dmPelsHeight;
        public int dmDisplayFlags;
        public int dmDisplayFrequency;
        public int dmICMMethod;
        public int dmICMIntent;
        public int dmMediaType;
        public int dmDitherType;
        public int dmReserved1;
        public int dmReserved2;
        public int dmPanningWidth;
        public int dmPanningHeight;
    }

    [StructLayout( LayoutKind.Sequential, CharSet = CharSet.Auto )]
    private struct MONITORINFOEX
    {
        public int cbSize;
        public RECT rcMonitor;
        public RECT rcWork;
        public int dwFlags;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string szDevice;
    }

    [StructLayout( LayoutKind.Sequential )]
    private struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    [DllImport( "user32.dll" )]
    private static extern IntPtr MonitorFromWindow( IntPtr hwnd, uint dwFlags );

    [DllImport( "user32.dll", CharSet = CharSet.Auto )]
    private static extern bool GetMonitorInfo( IntPtr hMonitor, ref MONITORINFOEX lpmi );

    [DllImport( "user32.dll", CharSet = CharSet.Auto )]
    private static extern bool EnumDisplaySettings( string deviceName, int modeNum, ref DEVMODE devMode );

    private const uint MONITOR_DEFAULTTONEAREST = 2;

    public static int GetWindowRefreshRate( RenderWindow window )
    {
        try
        {
            IntPtr hwnd = window.SystemHandle;
            IntPtr monitor = MonitorFromWindow(hwnd, MONITOR_DEFAULTTONEAREST);

            MONITORINFOEX monitorInfo = new MONITORINFOEX();
            monitorInfo.cbSize = Marshal.SizeOf( typeof( MONITORINFOEX ) );
            if ( !GetMonitorInfo( monitor, ref monitorInfo ) )
                return 60; // fallback

            DEVMODE devMode = new DEVMODE();
            devMode.dmSize = ( short )Marshal.SizeOf( typeof( DEVMODE ) );
            if ( EnumDisplaySettings( monitorInfo.szDevice, ENUM_CURRENT_SETTINGS, ref devMode ) )
            {
                if ( devMode.dmDisplayFrequency > 1 )
                    return devMode.dmDisplayFrequency;
            }

            return 60; // fallback
        }
        catch
        {
            return 60; // fallback if anything fails
        }
    }
}