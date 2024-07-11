using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        string mongoUri = Environment.GetEnvironmentVariable("MONGO_URI")!;

        var client = new MongoClient(mongoUri);
        var database = client.GetDatabase("RDProvincesDB");
        var collection = database.GetCollection<BsonDocument>("Provinces");

        // Datos de provincias de la República Dominicana
        var provinces = new List<BsonDocument>
        {
            new BsonDocument { { "name", "Santo Domingo" }, { "capital", "Santo Domingo" }, { "population", 3273919 } },
            new BsonDocument { { "name", "Santiago" }, { "capital", "Santiago de los Caballeros" }, { "population", 1320124 } },
            new BsonDocument { { "name", "La Vega" }, { "capital", "Concepción de La Vega" }, { "population", 394205 } },
            new BsonDocument { { "name", "San Cristóbal" }, { "capital", "San Cristóbal" }, { "population", 648039 } }
            // Agrega más provincias según sea necesario
        };

        // Insertar provincias en la colección
        await collection.InsertManyAsync(provinces);

        // Consultar todas las provincias y mostrarlas en la consola
        var allProvinces = await collection.Find(new BsonDocument()).ToListAsync();
        foreach (var province in allProvinces)
        {
            Console.WriteLine(province.ToString());
        }
    }
}
