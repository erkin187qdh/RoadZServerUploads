using RabbitMQ.Client;
using System;
using System.Text;

namespace AnalyseRoadZ
{
    class Program
    {
        public static readonly string HostName = "i.hate.software";
        public static ConnectionFactory connectionFactory = new ConnectionFactory
        {
            HostName = HostName,
            UserName = "roadz",
            Password = "HA77!ngs#"
        };

        static void Main(string[] args)
        {
            
            

            var connection = connectionFactory.CreateConnection();
            var channel = connection.CreateModel();





            MessageSender messageSender1 = new MessageSender("NewUserData");
            messageSender1.startSending("kindergeld");
            messageSender1.startSending("kindergeld2");
            messageSender1.startSending("kindergeld3");
            messageSender1.startSending("kindergeld4");
            messageSender1.startSending("kindergeld5");
            messageSender1.startSending("kindergeld6");



            //channel.BasicQos(0, 1, false);              //empfangen
            //MessageReceiver messageReceiver = new MessageReceiver("NewUserData");

            //messageReceiver.startReceiving();


            Console.ReadKey();
        }



        
    }
}
