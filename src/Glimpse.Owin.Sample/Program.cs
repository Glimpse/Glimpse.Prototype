using System;
using Microsoft.Owin.Hosting;

namespace Glimpse.Owin.Sample
{
    public class Program
    {
        public void Main(string[] args)
        {
            var port = 0;
            do
            {
                Console.Write("Please enter a port for the web application : ");
                var enteredPort = Console.ReadLine();
                if (!int.TryParse(enteredPort, out port))
                {
                    Console.WriteLine("Invalid port");
                }
            } while (port == 0);

            using (WebApp.Start<Startup>("http://localhost:" + port))
            {
                Console.WriteLine();
                Console.WriteLine("Started at http://localhost:" + port);
                Console.WriteLine("Press <ENTER> to stop the application");
                Console.ReadLine();
                Console.WriteLine("Stopping");
            }
        }
    }
}
