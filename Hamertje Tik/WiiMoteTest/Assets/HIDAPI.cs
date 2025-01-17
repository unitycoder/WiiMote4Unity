﻿using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

namespace Assets
{
    public class HIDException : Exception
    {
        public HIDException(string msg) : base(msg) { }
        public HIDException(Exception e) : base(e.Message) { }
    }

    public abstract class HIDAPI
    {
        public static HIDAPI GetAPI()
        {
            if (SystemInfo.operatingSystem.Contains("Windows"))
                return new WindowsHID();
            else if (SystemInfo.operatingSystem.Contains("Linux"))
            {
                Debug.Log("LINUX");
                return new LinuxHID();
            }
            else return null;
        }
        public abstract List<HIDDevice> GetDevices();

        public abstract void GetDeviceInfo(IntPtr dev_Handle, out int inputLength, out int outputLength);
        public abstract IntPtr Connect(string dev_Path);
        public abstract bool Disconnect(IntPtr device);
        public abstract uint Read(HIDDevice device, byte[] buffer, uint cbToRead);

        public abstract uint Write(HIDDevice device, byte[] buffer, uint cbToWrite);

        protected static void HandleException(Exception e, string message)
        {
            throw new HIDException("Error - " + message + " - found: " + e.Message + " Win32Error: " + Marshal.GetLastWin32Error());
        }
    }    
}
