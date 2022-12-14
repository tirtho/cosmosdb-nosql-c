using System;
using Microsoft.Azure.Cosmos;

string connectionString = "AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";

CosmosClient client = new (connectionString);
Database database = await client.CreateDatabaseIfNotExistsAsync("cosmosworks");
Console.WriteLine($"New Database:\tId: {database.Id}");

Container container = await database.CreateContainerIfNotExistsAsync("products", "/categoryId", 400);

string itemId = "706cd7c6-db8b-41f9-aea2-0e0c7e8eb009";
string itemCategoryId = "9603ca6c-9e28-4a02-9194-51cdb7fea816";

// Add an item in the container
Product aSaddle = new()
{
    id = itemId,
    categoryId = itemCategoryId,
    name = "Road Saddle",
    price = 45.99d,
    tags = new string[]
    {
        "tan",
        "new",
        "crisp"
    }
};

await container.CreateItemAsync<Product>(aSaddle);

// Read the item from the container
PartitionKey partitionKey = new PartitionKey(itemCategoryId);

Product theSaddle = await container.ReadItemAsync<Product>(itemId, partitionKey);

Console.WriteLine($"[{theSaddle.id}]\t{theSaddle.name} ({theSaddle.price:C})");

// Update item
theSaddle.name = "Road LL Saddle";
theSaddle.price = 32.55d;

Product theUpdatedSaddle = await container.UpsertItemAsync<Product>(theSaddle);
Console.WriteLine($"[{theUpdatedSaddle.id}]\t{theUpdatedSaddle.name} ({theUpdatedSaddle.price:C})");

// Delete item
await container.DeleteItemAsync<Product>(itemId, partitionKey);