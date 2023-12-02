using Microsoft.EntityFrameworkCore;
using WebAPI.DTO;
using WebAPI.Models;

namespace WebAPI.DAL;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;
    
    public ProductRepository(ApplicationDbContext dbContext)
    {
        _context = dbContext;
    }
    

    public List<Store>? GetStoresByIds(List<Guid> requestStoresIds)
    {
        var stores = _context.Stores.Where(s => requestStoresIds.Contains(s.StoreId)).ToList();
        if (stores.Count != requestStoresIds.Count)
        {
            return null;
        }

        return stores;
    }

    public void AddProduct(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
        
    }

    public bool GroupExists(Guid? requestGroupId)
    {
        return _context.Groups.Any(g => g.GroupId == requestGroupId);
    }

    public ProductResponse? GetProductById(Guid id)
    {
        return _context.Products
            .Where(p => p.ProductId == id)
            .Include(p => p.Group)
            .Include(p => p.Stores)
            .Select(p => new ProductResponse
            {
                ProductName = p.ProductName,
                GroupName = p.Group.GroupName,
                Price = p.Price,
                PriceWithVAT = p.PriceWithVAT,
                VatRate = p.VatRate,
                Time = p.TimeAdded,
                StoresNames = p.Stores.Select(s => s.StoreName).ToList()
            })
            .FirstOrDefault();
    }

    public List<ProductResponse> GetAllProducts()
    {
        return _context.Products
            .Include(p => p.Group)
            .Include(p => p.Stores)
            .Select(p => new ProductResponse
            {
                ProductName = p.ProductName,
                GroupName = p.Group.GroupName,
                Price = p.Price,
                PriceWithVAT = p.PriceWithVAT,
                VatRate = p.VatRate,
                Time = p.TimeAdded,
                StoresNames = p.Stores.Select(s => s.StoreName).ToList()
            })
            .ToList();
            
    }

    public List<GroupTree> GetAllGroups()
    {
        return _context.Groups
            .Select(p => new GroupTree()
            {
                Name = p.GroupName,
                GroupId = p.GroupId,
                ParentGroupId = p.ParentGroupId
            })
            .ToList();
    }
}