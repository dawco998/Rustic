using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace covet.cc.Rust
{
    class Rust
    {
        public static Int32 GOMAddress = 0x17C1F18;
        public static Int32 BaseNetworkable = 0x48944072;// 0x3065670 0x2FC3FC8
        public static IntPtr GameAssembly;

        public static int visiblePlayerList = 0x8;
        public static IntPtr BaseAddress;
        public static IntPtr i;
        public static UInt64 Camera;

      public static UInt64 GetCamera()
      {
            /* Read into the first entry in the list */
            UInt64 address = Memory.Memory.ReadMemory<UInt64>(GOMAddress + 0x8);

            /* Loop until we hit tag 5, which is camera */
            while (true)
            {
                /* Read into the GameObject */
                UInt64 game_object = Memory.Memory.ReadMemory<UInt64>((int)address + 0x10);

                /* Read this object's tag */
                Int16 tag = Memory.Memory.ReadMemory<Int16>((int)game_object + 0x54);

                if (tag == 5)
                {
                    List<int> dda = new List<int>();
                    dda.Add(0x30);
                    dda.Add(0x18);
                    return Memory.Memory.ReadChain<UInt64>(game_object, dda); 
                }

                /* Read into the next entry */
                address = Memory.Memory.ReadMemory<UInt64>((int)address + 0x8);
            }

            return address;
      }

    }
}
