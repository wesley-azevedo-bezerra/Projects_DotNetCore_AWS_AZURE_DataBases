using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Runtime;
using DynamoDB_DataBase.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DynamoDB_DataBase
{
    public static class DocumentModelSample
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


            using (IAmazonDynamoDB db = new AmazonDynamoDBClient(credentials, RegionEndpoint.SAEast1))
            {

                Table userTable = Table.LoadTable(db, "Users", DynamoDBEntryConversion.V2);
                Document newUser = new Document();
                newUser["Id"] = user.Email;
                newUser["FirstName"] = user.FirstName;
                newUser["LastName"] = user.LastName;
                newUser["Address"] = user.Address;
                newUser["Active"] = user.Active;
                newUser["NumberOfChildren"] = user.NumberOfChildren;
                newUser["Interests"] = user.Interests;


                await userTable.PutItemAsync(newUser);

                Console.WriteLine("User Saved");

                Document loadedUser = await userTable.GetItemAsync(user.Email);



                Console.WriteLine("Reading User");

            }

        }

 
    }


}
