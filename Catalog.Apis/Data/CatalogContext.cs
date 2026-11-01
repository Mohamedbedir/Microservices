using Catalog.Apis.Entities;
using Catalog.Apis.Shared;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Catalog.Apis.Data
{
    public class CatalogContext: ICatalogContext
    {
        private readonly DatabaseSettings Settings;

        public CatalogContext(IOptions<DatabaseSettings> databaseSettings)
        {
            this.Settings = databaseSettings.Value;

            var mongoClient = new MongoClient(Settings.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(Settings.DatabaseName);
            Products = mongoDatabase.GetCollection<Product>(Settings.CollectionName);

            // Seed Data
            CatalogSeedData.SeedData(Products);

        }

        public IMongoCollection<Product> Products { get; set; }
    }
}
