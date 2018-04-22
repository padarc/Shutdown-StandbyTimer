using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Shutdown_StandbyTimer
{
    class Program
    {
        static int Choice;
        static int MinuteInput;

        //Import for standby
        [DllImport("Powrprof.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool SetSuspendState(bool hiberate, bool forceCritical, bool disableWakeEvent);
        //

        static void Main(string[] args)
        {
            SetWindowSize(40, 10);
            ClearConsoleAndDisplayHeader();
            ReadChoice();
            ReadMinuteInput();
            StartTimer();
        }

        static void SetWindowSize(int Width, int Height)
        {
            Console.SetWindowSize(Width, Height);
            Console.SetBufferSize(Width, Height);
        }

        static void ClearConsoleAndDisplayHeader()
        {
            Console.Clear();
            Console.WriteLine("#########################");
            Console.WriteLine("# Shutdown+StandbyTimer #");
            Console.WriteLine("#########################");
            Console.WriteLine();
        }

        static void ReadChoice()
        {
            ClearConsoleAndDisplayHeader();
            Console.Write("Type 1 for Standby, 2 for Shutdown: ");
            Choice = Convert.ToInt32(System.Console.ReadLine());
        }

        static void ReadMinuteInput()
        {
            ClearConsoleAndDisplayHeader();
            if (Choice == 1)
            {
                Console.Write("How many minutes until standby?: ");
                MinuteInput = Convert.ToInt32(System.Console.ReadLine());
            }

            else if (Choice == 2)
            {
                Console.Write("How many minutes until shutdown?: ");
                MinuteInput = Convert.ToInt32(System.Console.ReadLine());
            }

            else
            {
                ClearConsoleAndDisplayHeader();
                Console.WriteLine("Wrong input, please try again!");
                Main(null);
            }
        }

        public static void StartTimer()
        {
            int MsRemaining =  MinuteInput * 60000;
            ClearConsoleAndDisplayHeader();
            Console.Write("Minutes remaining: " + MsRemaining / 60000);
            while (MsRemaining > 0)
            {
                System.Threading.Thread.Sleep(60000);
                MsRemaining = MsRemaining - 60000;
                ClearConsoleAndDisplayHeader();
                Console.Write("Minutes remaining: " + MsRemaining / 60000);
            }

            if (Choice == 1)
            {
                ClearConsoleAndDisplayHeader();
                InitializeStandby();
            }

            else if (Choice == 2)
            {
                ClearConsoleAndDisplayHeader();
                InitializeShutdown();
            }
        }

        static void InitializeStandby()
        {
            Console.WriteLine("Initializing Standby...");
            SetSuspendState(false, true, true);
            Console.WriteLine("All done, press any key to exit!");
            String s = Console.ReadLine();

        }

        static void InitializeShutdown()
        {
            Console.WriteLine("Initializing Shutdown...");
            Process.Start("shutdown", "/s /t 0");
            Console.WriteLine("All done, press any key to exit!");
            String s = Console.ReadLine();
        }
    }
}
