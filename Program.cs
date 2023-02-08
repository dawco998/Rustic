using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace covet.cc
{
    static class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        [STAThread]
        static void Main()
        {
          //  Memory.Memory.kernel = new Memory.Kernel();

          //  new Thread(Antis.Antis.AntiThread).Start();
            

            Process[] process = Process.GetProcessesByName("RustClient.exe");
            if (process.Length > 0)
            {
                Memory.Memory.ProcessHandle = process[0].Handle;

                Console.ForegroundColor = ConsoleColor.Green;


                Process MainProc = process[0];

            //    Memory.Kernel.SetProcessID((uint)MainProc.Id);

               foreach(ProcessModule mod in MainProc.Modules)
                {
                   if( mod.ModuleName == "GameAssembly.dll")
                    {
                        Rust.Rust.GameAssembly = mod.BaseAddress;
                    }
                    if (mod.ModuleName == "UnityPlayer.dll")
                    {
                        Rust.Rust.BaseAddress = mod.BaseAddress;
                    }
                }


            }

            Console.Beep(1000, 100);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Menu());

            

        }
    }
}
