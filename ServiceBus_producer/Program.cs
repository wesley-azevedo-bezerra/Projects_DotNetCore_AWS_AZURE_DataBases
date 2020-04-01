using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using ServiceBus_producer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBus_producer
{
    class Program
    {
        static ITopicClient topicClient;

        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }


        static async Task MainAsync()
        {
            string ServiceBusConnectionString = "Endpoint=sb://users-service-bus.servicebus.windows.net/;SharedAccessKeyName=user-send;SharedAccessKey=gcM1VLyebHf1AKbFuCe12xIFyBAmcDmwG994Zz4c0rM=";
            string TopicName = "users-topic";

            topicClient = new TopicClient(ServiceBusConnectionString, TopicName);


            //Send messages
            await SendUserMessage();

            Console.ReadKey();
            await topicClient.CloseAsync();

        }








        static async Task SendUserMessage()
        {
            //User user = new User()
            //{
            //    Active = true,
            //    Address = "Rua Vicente Savi , n12",
            //    Email = "wesleyazevedobezerra@gmail.com",
            //    FirstName = "wesley",
            //    LastName = "azevedo bezerra",
            //    NumberOfChildren = 0
            //};




            var message = "funciona";

       


            var serializeBody = JsonConvert.SerializeObject(message);


            var busMessage = new Message(Encoding.UTF8.GetBytes(serializeBody));
            
            await topicClient.SendAsync(busMessage);

            Console.WriteLine("Mensagem foi enviada!");


        }













    }
}

