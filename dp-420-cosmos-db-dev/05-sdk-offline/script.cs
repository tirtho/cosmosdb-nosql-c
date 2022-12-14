using System;
using Microsoft.Azure.Cosmos;

string connectionString = "AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";

CosmosClient client = new (connectionString);
Database database = await client.CreateDatabaseIfNotExistsAsync("cosmosworks");
Console.WriteLine($"New Database:\tId: {database.Id}");

Container container = await database.CreateContainerIfNotExistsAsync("products", "/categoryId", 400);
Console.WriteLine($"New Container:\tId: {container.Id}");


