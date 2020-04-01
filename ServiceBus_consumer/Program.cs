using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using ServiceBus_consumer.Entities;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceBus_consumer
{

        class Program
        {
            static ISubscriptionClient subscriptionClient;

            static void Main(string[] args)
            {
                Console.WriteLine("Listner started.");
                MainAsync().GetAwaiter().GetResult();
            }

            static async Task MainAsync()
            {
                string ServiceBusConnectionString = "Endpoint=sb://users-service-bus.servicebus.windows.net/;SharedAccessKeyName=user-listen;SharedAccessKey=a4+PqmOcHKt0CoPBiDa+97tdsa86eZSbboqzrnoQmxs=";
                string TopicName = "users-topic";
                string SubscriptionName = "user-topic-subscription";

                subscriptionClient = new SubscriptionClient(ServiceBusConnectionString, TopicName, SubscriptionName);

                // Register subscription message handler and receive messages in a loop.
                RegisterOnMessageHandlerAndReceiveMessages();

                Console.ReadKey();

                await subscriptionClient.CloseAsync();
            }

            static void RegisterOnMessageHandlerAndReceiveMessages()
            {
                var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler);

                // Register the function that processes messages.
                subscriptionClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
            }

            static async Task ProcessMessagesAsync(Message message, CancellationToken token)
            {
                var messageBody = Encoding.UTF8.GetString(message.Body);


                Console.WriteLine($"Received message: UserInfo:{messageBody}");

                await subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
            }

            static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
            {
                var exception = exceptionReceivedEventArgs.Exception;

                return Task.CompletedTask;
            }


        }
    }