using System;

namespace DB_Connector
{
    class Program 
    {
        static void Main(string[] args)
        {
           
            Console.WriteLine("Hello World!");
            Connector connector = new Connector();
            Worker worker = new Worker(connector);
            //connector.readDataReturn += receiveDataDel;

        }


    }

}
