using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace AnalyseRoadZ
{
    class MessageReceiver
    {
        private readonly string queueName;
        private IModel channel;
        private IConnection connection;
        ConnectionFactory factory;
        EventingBasicConsumer consumer;
        int messageCounter = 0;

        public MessageReceiver(string queueName)
        {
            this.queueName = queueName;
            buildConnection();
        }
        private void buildConnection()
        {
            factory = new ConnectionFactory() { HostName = "localhost" };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
            consumer = new EventingBasicConsumer(channel);
        }

        public void startReceiving()
        {
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($" [{queueName}] Received {message}", message);
                Console.WriteLine("Nächste Nachricht? [j/n]:            " + messageCounter++);
            };

            channel.BasicQos(0, 1, false);
            channel.BasicConsume(queue: queueName,
                                 autoAck: true,
                                 consumer: consumer);

        }



       
    }
}
