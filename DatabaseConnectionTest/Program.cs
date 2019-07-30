using System;

namespace DatabaseConnectionTest
{
    class Program
    {
        static void Main(string[] args)
        {
            DBConnect.connectDB();
            Console.WriteLine(DBConnect.serverVersion() + "\n\n");
            DBConnect.closeDB();  

            Console.WriteLine("Press Any Key To Exit");
            Console.ReadKey();
        }
        }
    }