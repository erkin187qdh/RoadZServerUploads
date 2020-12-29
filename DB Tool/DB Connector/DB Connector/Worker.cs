using System;
using System.Collections.Generic;
using System.Text;

namespace DB_Connector
{
    class Worker : IReadDataReturn
    {
        Connector connector;

        public Worker(Connector connector)
        {
            this.connector = connector;
        }

        public void subscribe()
        {
            connector.subscribeListener(this);
            Connector.readDataReturn += receiveDataDel;
        }

        public void receiveData(string data)
        {
            Console.WriteLine(data);
        }

        public static void receiveDataDel(string data)
        {
            Console.WriteLine(data);
        }
    }
}
