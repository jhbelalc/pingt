using System;
using System.Net.NetworkInformation;
using System.Timers;

namespace pingt
{
    class Program
    {
        static bool _cancelled;
        static void Main(string[] args)
        {  
            try
            {
                string cHost = "";
                int nPing = 0, nError = 0;
                if (args.Length>0)
                {
                    cHost = args[0];
                } 
                if (string.IsNullOrEmpty(cHost))
                {
                    cHost = "www.google.com";
                }

                Ping pPing = new Ping();
                Console.WriteLine(System.DateTime.Now.ToString() + " Ping to " + cHost + " Press CTRL+C to exit");
                Console.CancelKeyPress += new ConsoleCancelEventHandler(Console_CancelKeyPress);
                while (true)
                {
                    PingReply reply = pPing.Send(cHost, 1000);
                    nPing++;
                    if (reply != null)
                    {
                        Console.WriteLine(System.DateTime.Now.ToString() + " Reply from " + reply.Address + " bytes=32 Time: " + reply.RoundtripTime.ToString() + " Status: " + reply.Status);
                    }
                    else
                    {
                        Console.WriteLine(System.DateTime.Now.ToString() + "No response");
                        nError++;
                    }
                    System.Threading.Thread.Sleep(1000);
                    if (_cancelled)
                    {
                        Console.WriteLine("Total pings: " + nPing.ToString() + " Errors: " + nError.ToString());
                        Environment.Exit(0);
                    };
                }
            }
            catch (InvalidCastException e)
            {
                Console.WriteLine("ERROR in Pingt "+e.Message);
            }

            Console.ReadKey();
        }

        private static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            if (e.SpecialKey == ConsoleSpecialKey.ControlC)
            {
                _cancelled = true;
                e.Cancel = true;
            }
        }
    }
}
