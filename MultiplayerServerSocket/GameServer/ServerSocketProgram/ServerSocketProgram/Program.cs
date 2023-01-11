using System;

namespace ServerSocketProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Server to connect");
            Server.Start(5, 11000); 

            Console.ReadLine();
        }
    }
}
