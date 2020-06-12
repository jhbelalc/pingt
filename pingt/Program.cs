using System;
using System.Net.NetworkInformation;

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
                        if (reply.Status!=IPStatus.Success){
                            Console.ForegroundColor=ConsoleColor.Red;
                            nError++;
                        }
                        Console.WriteLine(System.DateTime.Now.ToString() + " Reply from " + reply.Address + " bytes=32 Time: " + reply.RoundtripTime.ToString() + " Status: " + reply.Status);
                        if (Console.ForegroundColor!=ConsoleColor.White){
                            Console.ForegroundColor=ConsoleColor.White;
                        }
                    }
                    else
                    {
                        Console.ForegroundColor= ConsoleColor.Red;
                        Console.WriteLine(System.DateTime.Now.ToString() + "No response");
                        Console.ForegroundColor=ConsoleColor.White;
                        nError++;
                    }
                    System.Threading.Thread.Sleep(1000);
                    if (_cancelled)
                    {
                        Console.ForegroundColor=ConsoleColor.Green;
                        Console.WriteLine("Total pings: " + nPing.ToString() + " Errors: " + nError.ToString());
                        Console.ForegroundColor=ConsoleColor.White;
                        Environment.Exit(0);
                    };
                }
            }
            catch (InvalidCastException e)
            {
                Console.ForegroundColor=ConsoleColor.Red;
                Console.WriteLine("ERROR in Pingt "+e.Message);
                Console.ForegroundColor=ConsoleColor.White;
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
