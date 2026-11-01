using Catalog.Apis.Data;
using Catalog.Apis.Entities;
using MongoDB.Driver;

namespace Catalog.Apis.Repository
{
    public class ProductRepo : IProductRepo
    {
        private readonly ICatalogContext _Context;

        public ProductRepo(ICatalogContext Context)
        {
            _Context = Context;
        }

        public async Task CreateProduct(Product product)
        {
            await _Context.Products.InsertOneAsync(product);
        }

        public async Task<bool> DeleteProduct(string id)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            var result = await _Context.Products.DeleteOneAsync(filter);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        public async Task<Product> GetProduct(string id)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            return await _Context.Products.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string category)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Category, category);
            return await _Context.Products.Find(filter).ToListAsync();
        }
        

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            var filter = Builders<Product>.Filter.Regex(p => p.Name, new MongoDB.Bson.BsonRegularExpression(name,"i"));
            return await _Context.Products.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _Context.Products.Find(p => true).ToListAsync();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, product.Id);
            var result = await _Context.Products.ReplaceOneAsync(filter, product);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }
        
    }
}
