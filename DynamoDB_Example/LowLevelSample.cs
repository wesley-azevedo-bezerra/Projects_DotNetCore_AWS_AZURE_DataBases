using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
using DynamoDB_DataBase.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DynamoDB_DataBase
{
    public static class LowLevelSample
    {

        public static async Task ExecuteAsync()
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

            var credentials = new BasicAWSCredentials("AKIAZEFCLWDDRHQZKD7U", "0D4KlTb4COgrY8hupdQPnbBANTRwQP448I8nTxGw");


            using (IAmazonDynamoDB  db = new AmazonDynamoDBClient(credentials, RegionEndpoint.SAEast1))
            {

                await db.PutItemAsync(new PutItemRequest()
                {
                    TableName = "Users",
                    Item = new Dictionary<string, AttributeValue>
                    {
                        { "Id", new AttributeValue{ S = user.Email }},
                        { "FirstName", new AttributeValue{ S = user.FirstName }},
                        { "LastName", new AttributeValue{ S = user.LastName }},
                        { "Address", new AttributeValue{ S = user.Address }},
                        { "Active", new AttributeValue{ BOOL = user.Active }},
                        { "NumberOfChildren", new AttributeValue{ N = user.NumberOfChildren.ToString()}},
                        { "Interests", new AttributeValue { SS = user.Interests }}

                }
                });

                Console.WriteLine("User Saved");

                Dictionary<string, AttributeValue> item = (await db.GetItemAsync(new GetItemRequest(){ 
                    
                    TableName = "Users",
                    ConsistentRead = true,
                    Key = new Dictionary<string, AttributeValue>
                    {
                        { "Id", new AttributeValue{ S = user.Email }}
                    }

                })).Item;

                Console.WriteLine("Reading User");


                await db.DeleteItemAsync(new DeleteItemRequest
                {
                    TableName = "Users",
                    Key = new Dictionary<string, AttributeValue>
                    {
                        { "Id",  new AttributeValue{ S = user.Email } }
                    }
                });

                Console.WriteLine("User Deleted");








            }
        }
    }
}
