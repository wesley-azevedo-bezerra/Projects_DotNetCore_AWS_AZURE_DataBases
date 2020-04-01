using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using SQS_consumer.Entities;
using System;
using System.Linq;
using System.Text.Json;

namespace SQS_consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var sqs = new AmazonSQSClient(RegionEndpoint.SAEast1);

            var queueUrl = sqs.GetQueueUrlAsync("User").Result.QueueUrl;


            var receiveMessageRequest = new ReceiveMessageRequest
            {
                QueueUrl = queueUrl
            };

            var receiveMessageResponse = sqs.ReceiveMessageAsync(receiveMessageRequest).Result;

            foreach (var message in receiveMessageResponse.Messages)
            {
                Console.WriteLine("Message \n");
                Console.WriteLine($"  MessageId: {message.MessageId}");
                Console.WriteLine($"  ReceiptHandle: {message.ReceiptHandle}");
                Console.WriteLine($"  MSD5Body {message.MD5OfBody} \n");
                Console.WriteLine($"  Body {message.Body} \n");


                User user  = JsonSerializer.Deserialize<User>(message.Body);




                var messageReceiptHandle = receiveMessageResponse.Messages.FirstOrDefault()?.ReceiptHandle;

                var deleteRequest = new DeleteMessageRequest
                {
                    QueueUrl = queueUrl,
                    ReceiptHandle = messageReceiptHandle
                };

                sqs.DeleteMessageAsync(deleteRequest);

            }
            Console.ReadLine();


        }
    }
}
