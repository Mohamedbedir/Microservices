using Catalog.Apis.Entities;
using MongoDB.Driver;

namespace Catalog.Apis.Data
{
    public interface ICatalogContext
    {
        public IMongoCollection<Product> Products { get; set; }

    }
}