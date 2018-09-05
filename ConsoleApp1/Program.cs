using System;
using System.Diagnostics;
using KbgSoft.LineCounter;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--------");

            Console.ReadKey();
        }




        private static Stats StatFolder(string path)
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine();
                var w = Stopwatch.StartNew();
                var files = new LineCounting().GetFiles(path);
                var res = new LineCounting().CountFiles(files);
                PrintStats(res, w, path);
                return res;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return null;
        }

        private static void PrintStats(Stats res, Stopwatch w, string path)
        {
            Console.WriteLine(path);

            Console.WriteLine("files, " + res.Files);
            Console.WriteLine("Time: " + w.ElapsedMilliseconds / 1000d);
            Console.WriteLine(res.Print());
        }
    }
}
