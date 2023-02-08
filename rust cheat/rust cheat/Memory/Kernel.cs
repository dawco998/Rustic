using System;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32.SafeHandles;

namespace covet.cc.Memory
{
    class Kernel
    {
        [DllImport("covet.cc dll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void kernel_control_function();

        [DllImport("covet.cc dll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetProcessID(uint ID);


        [DllImport("covet.cc dll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt64 GetBaseAddress();

        [DllImport("covet.cc dll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt64 GetUnityPlayer();

        [DllImport("covet.cc dll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt64 GetGameAssembly();


        public Kernel()
        {
            kernel_control_function();
        }
        

    }
}
