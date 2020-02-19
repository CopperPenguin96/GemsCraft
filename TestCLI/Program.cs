using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft;
using GemsCraft.AppSystem;
using GemsCraft.GUI;

namespace TestCLI
{
    class Program
    {
        static void Main(string[] args)
        {
           /* Server.InitServer(false);
            Server.Start();*/
            new SplashScreen().ShowDialog();
            Console.ReadLine();
        }

    }
}
