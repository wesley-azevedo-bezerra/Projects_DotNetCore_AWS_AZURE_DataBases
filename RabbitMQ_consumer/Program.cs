using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ_consumer.Entities;
using System;
using System.Text;
using System.Text.Json;

namespace RabbitMQ_consumer
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



                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, message) =>
                    {
                        var body = message.Body;
                        var message_return = Encoding.UTF8.GetString(body);

                        User user = JsonSerializer.Deserialize<User>(message_return);



                        Console.WriteLine($"Mensagem Recebida  ==> {message_return} ");

                    };

                    channel.BasicConsume(queue: "User",
                                         autoAck: true,
                                         consumer: consumer);

                    connection.Close();
                    Console.WriteLine($"Press [enter] to exit.");
                    Console.ReadLine();

                }
            }
        }
 

