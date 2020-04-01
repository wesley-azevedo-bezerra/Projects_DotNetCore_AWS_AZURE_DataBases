using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using DynamoDB_DataBase.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DynamoDB_DataBase
{
    public  static class DataModelSample
    {

        public static async Task ExecuteAsync()
        {

            var credentials = new BasicAWSCredentials("AKIAZEFCLWDDRHQZKD7U", "0D4KlTb4COgrY8hupdQPnbBANTRwQP448I8nTxGw");


      

                AmazonDynamoDBClient client = new AmazonDynamoDBClient(credentials, RegionEndpoint.SAEast1);

            using (DynamoDBContext context = new DynamoDBContext(client))
            {
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

                await context.SaveAsync(user);

                Console.WriteLine("User Saved");

                User loadUser = await context.LoadAsync<User>(user.Email);


                Console.WriteLine("Reading User");
                PrintUser(loadUser);

                await context.DeleteAsync<User>(user.Email);

                Console.WriteLine("User Delete");



            }


        }

        public static void PrintUser(User user)
        {
            Console.WriteLine($"Id: {user.Email}");
            Console.WriteLine($"FirstName: {user.FirstName}");
            Console.WriteLine($"LastName: {user.LastName}");
            Console.WriteLine($"Address: {user.Address}");
            Console.WriteLine($"Active: {user.Active}");
            Console.WriteLine($"Interest Count: {user.Interests.Count}");
            
            foreach(var item in user.Interests)
            {
                Console.WriteLine($"Interest : {item}");

            }



        }

    }
}
