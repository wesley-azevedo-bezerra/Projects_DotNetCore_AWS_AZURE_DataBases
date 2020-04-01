using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using SQS_producer.Entities;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace SQS_producer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*********************************");
            Console.WriteLine("Amazon SQS");
            Console.WriteLine("*********************************\n");

            IAmazonSQS sqs = new AmazonSQSClient(RegionEndpoint.SAEast1);

            Console.WriteLine("Create a queue called EmailQueue.\n");

            var sqsRequest = new CreateQueueRequest
            {
                QueueName = "User",

            };


            var createQueueResponse = sqs.CreateQueueAsync(sqsRequest).Result;
            var myQueueUrl = createQueueResponse.QueueUrl;

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



            var sqsMessageRequest = new SendMessageRequest
            {
                QueueUrl = myQueueUrl,
                MessageBody = JsonSerializer.Serialize(user)
            };
            sqs.SendMessageAsync(sqsMessageRequest);

            Console.WriteLine("Finished sending message to SQS queue.\n");

            Console.ReadLine();





        }
    }
}
