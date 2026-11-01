using Catalog.Apis.Entities;

namespace Catalog.Apis.Repository
{
    public interface IProductRepo
    {
        Task CreateProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(string id);

        Task<Product> GetProduct(string id);
        Task<IEnumerable<Product>> GetProducts();
        Task<IEnumerable<Product>> GetProductByCategory(string category);
        Task<IEnumerable<Product>> GetProductByName(string name);

    }
}
