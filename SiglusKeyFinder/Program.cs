using System;
using System.Diagnostics;
using System.IO;

namespace SiglusKeyFinder
{
    internal class Program
    {
        internal static int PID { get; set; }

        private static void Main(string[] args)
        {
            if (args.Length != 1 || !File.Exists(args[0]))
            {
                Console.WriteLine("Failed");
                return;
            }

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = args[0],
                Arguments = string.Empty,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            using (Process process = new Process())
            {
                process.StartInfo = startInfo;

                try
                {
                    process.Start();
                    PID = process.Id;
                }
                catch
                {
                    Console.WriteLine("Failed");
                    process.Kill();
                    return;
                }

                try
                {
                    Console.WriteLine(BitConverter.ToString(KeyFinder.ReadProcess(PID)[0].KEY));
                }
                catch
                {
                    Console.WriteLine("Failed");
                }
                finally
                {
                    process.Kill();
                }
            }
        }
    }
}
