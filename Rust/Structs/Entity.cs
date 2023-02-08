using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace covet.cc.Rust.Structs
{
    class Entity
    {
        public UInt64 EntityBase;

        public Entity(UInt64 Base)
        {
            this.EntityBase = Base;
        }

        public float Health
        {
            get
            {
                try
                {
                    return Memory.Memory.Mem.ReadVirtualMemory<float>((IntPtr)EntityBase + 0x20C);
                }
                catch
                {
                    return 0;
                }
            }
        }

  

        public bool IsLocalPlayer
        {
            get
            {
                try
                {
                    UInt64 PlayerModel = Memory.Memory.Mem.ReadVirtualMemory<UInt64>((IntPtr)EntityBase + 0x4A8);

                    return Memory.Memory.Mem.ReadVirtualMemory<bool>((IntPtr)PlayerModel + 0x259);
                }
                catch
                {
                    return false;
                }
            }
        }
        public bool IsVisible
        {
            get
            {
                try
                {
                    UInt64 PlayerModel = Memory.Memory.Mem.ReadVirtualMemory<UInt64>((IntPtr)EntityBase + 0x4A8);

                    return Memory.Memory.Mem.ReadVirtualMemory<bool>((IntPtr)PlayerModel + 0x248);
                }
                catch
                {
                    return false;
                }
            }
        }

        public float Spider
        {
            get
            {

                UInt64 movement = Memory.Memory.Mem.ReadVirtualMemory<UInt64>((IntPtr)EntityBase + 0x4D0);// public BaseMovement movement;

                return Memory.Memory.Mem.ReadVirtualMemory<float>((IntPtr)movement + 0xB4);

            }
            set
            {
                UInt64 movement = Memory.Memory.Mem.ReadVirtualMemory<UInt64>((IntPtr)EntityBase + 0x4D0);// public BaseMovement movement;

                Memory.Memory.Mem.WriteVirtualMemory<float>((IntPtr)movement + 0xB8, 0f);
                Memory.Memory.Mem.WriteVirtualMemory<float>((IntPtr)movement + 0xB4, 0f);
            }
        }
       


        public Vector3 Position
        {
            get
            {
                try
                {
                    UInt64 PlayerModel = Memory.Memory.Mem.ReadVirtualMemory<UInt64>((IntPtr)EntityBase + 0x4A8);// public PlayerModel playerModel;

                    return Memory.Memory.Mem.ReadVirtualMemory<Vector3>((IntPtr)PlayerModel + 0x1D8);
                }
                catch
                {
                    return new Vector3(0, 0, 0);
                }
            }
        }

        public Vector3 Velocity
        {
            get
            {
                try
                {
                    UInt64 PlayerModel = Memory.Memory.Mem.ReadVirtualMemory<UInt64>((IntPtr)EntityBase + 0x4A8);//// public PlayerModel playerModel;

                    return Memory.Memory.Mem.ReadVirtualMemory<Vector3>((IntPtr)PlayerModel + 0x1f4);//velocity
                }
                catch
                {
                    return new Vector3(0, 0, 0);
                }
            }
        }
        

        public Vector2 ViewAngle
        {
            get
            {
             
                    UInt64 PlayerModel = Memory.Memory.Mem.ReadVirtualMemory<UInt64>((IntPtr)EntityBase + 0x4C8);// public PlayerInput input;

                return Memory.Memory.Mem.ReadVirtualMemory<Vector2>((IntPtr)PlayerModel + 0x3C);
               
            }
            set
            {           
                    UInt64 PlayerModel = Memory.Memory.Mem.ReadVirtualMemory<UInt64>((IntPtr)EntityBase + 0x4C8);// public PlayerInput input;

                Memory.Memory.Mem.WriteVirtualMemory<Vector2>((IntPtr)PlayerModel + 0x3C, value);                            
            }
        }
        public Vector2 RecoilAngle
        {
            get
            {

                UInt64 PlayerModel = Memory.Memory.Mem.ReadVirtualMemory<UInt64>((IntPtr)EntityBase + 0x4C8);// public PlayerInput input;

                return Memory.Memory.Mem.ReadVirtualMemory<Vector2>((IntPtr)PlayerModel + 0x64);

            }
            set
            {

                UInt64 PlayerModel = Memory.Memory.Mem.ReadVirtualMemory<UInt64>((IntPtr)EntityBase + 0x4C8);// public PlayerInput input;

                Memory.Memory.Mem.WriteVirtualMemory<Vector2>((IntPtr)PlayerModel + 0x64, value);


            }
        }

   
    public string Name
        {
            get
            {

                UInt64 PlayerModel = Memory.Memory.Mem.ReadVirtualMemory<UInt64>((IntPtr)EntityBase + 0x658);

                return Memory.Memory.ReadString((long)PlayerModel + 0x14 , 1);

            }
           
        }
      

     
    }

 
}
