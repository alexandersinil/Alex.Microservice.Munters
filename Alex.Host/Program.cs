using Microsoft.Owin.Hosting;
using System;
using Alex.Application;

namespace Alex.Host
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = "http://localhost:8088/";

            using (WebApp.Start<Startup>(baseAddress))
            {
                Console.WriteLine("Web server started");
                Console.ReadLine();
            }
        }
    }
}
