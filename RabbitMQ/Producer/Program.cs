using RabbitMQ.Client;
using System;
using System.Text;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using(var connection = factory.CreateConnection())
            {
                using(var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "hello",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    var shouldQuit = false;
                    Console.WriteLine("You may now type in your message");

                    while(!shouldQuit)
                    {
                        var message = Console.ReadLine();
                        shouldQuit = message == "q";

                        if(!shouldQuit)
                        {
                            var body = Encoding.UTF8.GetBytes(message);

                            channel.BasicPublish(exchange: "",
                                                 routingKey: "hello",
                                                 basicProperties: null,
                                                 body: body);

                            Console.WriteLine($"Sent '{message}'");
                        }
                    }
                }
            }
        }
    }
}
