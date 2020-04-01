using System;

namespace DynamoDB_DataBase
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Low Level Sample!");

            LowLevelSample.ExecuteAsync().Wait();

            Console.WriteLine("\n\n");

            Console.WriteLine("Document Model Sample");

            DocumentModelSample.ExecuteAsync().Wait();

            Console.WriteLine("\n\n");

            Console.WriteLine("Data Model Sample");

            DataModelSample.ExecuteAsync().Wait();



        }
    }
}
