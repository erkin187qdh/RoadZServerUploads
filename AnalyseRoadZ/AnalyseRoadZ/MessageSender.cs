using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnalyseRoadZ
{
    class MessageSender
    {
        private readonly string queueName;
        private IModel channel;
        private IConnection connection;
        ConnectionFactory factory;
        public MessageSender(string queueName)
        {
            this.queueName = queueName;
            this.buildConnection();
        }

        private void buildConnection()
        {
            factory = new ConnectionFactory() { HostName = "localhost" };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
            channel.QueueDeclare(queue: queueName,
                                 durable: false,
                                 exclusive: true,
                                 autoDelete: false,
                                 arguments: null);
        }

        public void startSending(string message)
        {


            using (channel = connection.CreateModel())
            {
                byte[] body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
                Console.WriteLine("message sent");
            }
        }
    }
}
