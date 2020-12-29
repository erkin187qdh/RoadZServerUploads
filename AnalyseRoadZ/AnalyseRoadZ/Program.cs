using RabbitMQ.Client;
using System;
using System.Text;

namespace AnalyseRoadZ
{
    class Program
    {
        static void Main(string[] args)
        {
            const string HostName = "localhost";

            ConnectionFactory connectionFactory = new ConnectionFactory
            {
                HostName = HostName
            };
            

            var connection = connectionFactory.CreateConnection();
            var channel = connection.CreateModel();

            channel.BasicQos(0, 1, false);              //empfangen
            MessageReceiver messageReceiver = new MessageReceiver("NewUserData");



            MessageSender messageSender1 = new MessageSender("NewUserData");
            messageSender1.startSending("kindergeld");
            messageSender1.startSending("kindergeld2");
            messageSender1.startSending("kindergeld3");
            messageSender1.startSending("kindergeld4");
            messageSender1.startSending("kindergeld5");
            messageSender1.startSending("kindergeld6");


            //messageReceiver.startReceiving();


            Console.ReadKey();
        }



        
    }
}
