using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemsCraft;
using GemsCraft.AppSystem;

namespace TestCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            Server.InitServer(false);
            Server.Start();
            Console.ReadLine();
        }

    }
}
