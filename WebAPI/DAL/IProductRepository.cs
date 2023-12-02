using WebAPI.DTO;
using WebAPI.Models;

namespace WebAPI.DAL;

public interface IProductRepository
{
    List<Store>? GetStoresByIds(List<Guid> requestStoresIds);

    void AddProduct(Product product);
    bool GroupExists(Guid? requestGroupId);
    
    ProductResponse? GetProductById(Guid id);
    List<ProductResponse> GetAllProducts();
    List<GroupTree> GetAllGroups();
}