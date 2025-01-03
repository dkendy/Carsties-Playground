using System.Text.Json;
using MongoDB.Driver;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Data;

public class DbInitializer
{
    private readonly SearchDbContext _searchDbContext;
    public DbInitializer(SearchDbContext searchDbContext){ 
        _searchDbContext = searchDbContext;
    }
    public static async Task InitDb(WebApplication app, SearchDatabaseSettings mongoDbSettings)
    {
 

          await DB.InitAsync("SearchDb", MongoClientSettings
        .FromConnectionString(mongoDbSettings.ConnectionString));

        await DB.Index<Item>()
            .Key(x => x.Make, KeyType.Text)
            .Key(x => x.Model, KeyType.Text)
            .Key(x => x.Color, KeyType.Text)
            .CreateAsync();

        await DB.Index<Document>()
            .Key(x => x.Name, KeyType.Text) 
            .CreateAsync();


        var count = await DB.CountAsync<Item>();

        if(count ==0){
            Console.WriteLine("No data - will attemp to seed");
            var itemData = await File.ReadAllTextAsync("Data/auctions.json");

            var options = new JsonSerializerOptions{PropertyNameCaseInsensitive = true};

            var items = JsonSerializer.Deserialize<List<Item>>(itemData, options);

            await DB.SaveAsync(items);
            

        }  

    }


}

