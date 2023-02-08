﻿using Jupiter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace covet.cc.Memory
{
    class Memory
    {
        public static Kernel kernel;

        public static MemoryModule Mem = new MemoryModule("RustClient.exe");

        public class StringStruct
        {
            public string pad = new string(new char[0x10]);
            public int strLength = new int();
            public string str = new string(new char[256]);
        }


        public struct RECT
        {
            public int left, top, right, bottom;
        }
        public static string WindName = "Rust";

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);

        [DllImport("User32.dll")]
        public static extern short GetAsyncKeyState(System.Windows.Forms.Keys vKey);

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(int hProcess, int lpBaseAddress, byte[] buffer, int size, ref int lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        private static extern bool WriteProcessMemory(int hProcess, int lpBaseAddress, byte[] buffer, int size, out int lpNumberOfBytesWritten);

        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(int vKey);

        public static Process Process;
        public static IntPtr ProcessHandle;
        public static IntPtr UnityPlayer;
        public static int m_iBytesRead = 0;
        public static int m_iBytesWrite = 0;

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool ReadProcessMemory(IntPtr hProcess, long lpBaseAddress, [In, Out] byte[] lpBuffer, ulong dwSize, out IntPtr lpNumberOfBytesRead);


        public static T ReadMemory<T>(int Adress) where T : struct
        {
            int ByteSize = Marshal.SizeOf(typeof(T));
            byte[] buffer = new byte[ByteSize];
            ReadProcessMemory((int)ProcessHandle, Adress, buffer, buffer.Length, ref m_iBytesRead);

            return ByteArrayToStructure<T>(buffer);
        }

        private static unsafe byte[] ReadMemory(IntPtr address, int numOfBytes, out long bytesRead)
        {
            byte[] buffer = new byte[numOfBytes];

            IntPtr pBytesRead = IntPtr.Zero;

            ReadProcessMemory((int)ProcessHandle, (int)address, buffer, buffer.Length, ref numOfBytes);

            bytesRead = pBytesRead.ToInt64();

            return buffer;
        }

     

        public static string ReadString(long baseAddress, ulong size)
        {
            //create buffer for string
            byte[] buffer = new byte[size];

            //read memory into buffer
            IntPtr bytesRead;
            ReadProcessMemory(ProcessHandle, baseAddress, buffer, size, out bytesRead);

            //encode bytes to ASCII
            return Encoding.ASCII.GetString(buffer);
        }

        public static UInt64 ReadChain<T>(UInt64 address, List<int> Ints) where T : struct
        {
            Type ByteSize = typeof(T);

            UInt64 current = address;
            for (int i = 0; i < Ints.ToArray().Length - 1; i++)
            {
                current = ReadMemory<UInt64>((int)current + Ints.ToArray()[i]);
            }
            return ReadMemory<UInt64>((int)current + Ints.ToArray()[Ints.ToArray().Length - 1]);
        }

        public static void WriteMemory<T>(int Adress, object Value)
        {
            byte[] buffer = StructureToByteArray(Value);

            WriteProcessMemory((int)ProcessHandle, Adress, buffer, buffer.Length, out m_iBytesWrite);
        }

        public static float[] ConvertToFloatArray(byte[] bytes)
        {
            if (bytes.Length % 4 != 0)
                throw new ArgumentException();

            float[] floats = new float[bytes.Length / 4];

            for (int i = 0; i < floats.Length; i++)
                floats[i] = BitConverter.ToSingle(bytes, i * 4);

            return floats;
        }

        public static T ByteArrayToStructure<T>(byte[] bytes) where T : struct
        {
            var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            try
            {
                return (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            }
            finally
            {
                handle.Free();
            }
        }

        public static byte[] StructureToByteArray(object obj)
        {
            int len = Marshal.SizeOf(obj);

            byte[] arr = new byte[len];

            IntPtr ptr = Marshal.AllocHGlobal(len);

            Marshal.StructureToPtr(obj, ptr, true);
            Marshal.Copy(ptr, arr, 0, len);
            Marshal.FreeHGlobal(ptr);

            return arr;
        }
    }
}
