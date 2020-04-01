using RabbitMQ.Client;
using RabbitMQ_producer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace RabbitMQ_producer
{
    class Program
    {
        static void Main(string[] args)
        {

            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
                Port = 5672,
                RequestedConnectionTimeout = 3000, // milliseconds
            };

            var connection = factory.CreateConnection();

            var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "User",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);




            User user = new User()
            {
                Active = true,
                Address = "Rua Vicente Savi , n12",
                Email = "wesleyazevedobezerra@gmail.com",
                FirstName = "wesley",
                LastName = "azevedo bezerra",
                Interests = new List<string>
                        {
                            "Futebol",
                            "Video Game",
                            "Gastronomia",
                            "Viajens"
                        },
                NumberOfChildren = 0
            };




            string message = JsonSerializer.Serialize(user);

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                                 routingKey: "User",
                                 basicProperties: null,
                                 body: body);

            Console.WriteLine($"Enviando a mensagem {message}");


            Console.WriteLine($"Press [enter] to exit.");
            Console.ReadLine();

        }
    }
}
