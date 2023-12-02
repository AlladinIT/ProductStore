using WebAPI.DAL;
using WebAPI.DTO;
using WebAPI.Models;
using WebAPI.Services.enums;

namespace WebAPI.Services;

public class ProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public ServiceResult AddProduct(AddProductRequest request)
    {
        
        if (!_productRepository.GroupExists(request.GroupId))
        {
            return ServiceResult.GroupNotFound;
        }

        List<Store>? stores = new List<Store>();
        
        if (request.StoresIds != null)
        {
            stores = _productRepository.GetStoresByIds(request.StoresIds);
            if (stores == null)
            {
                return ServiceResult.StoreNotFound;
            }
        }
        
        request.CalculateMissingValues();
        
        var product = new Product
        {
            GroupId = ((Guid)request.GroupId),
            Price = (decimal)request.Price,
            PriceWithVAT = (decimal)request.PriceWithVAT,
            TimeAdded = DateTime.Now,
            ProductName = request.ProductName,
            VatRate = (decimal)request.VatRate,
            Stores = stores
        };
        _productRepository.AddProduct(product);

        return ServiceResult.Success;

    }

    public ProductResponse? GetProduct(Guid? id)
    {
        return _productRepository.GetProductById((Guid)id!);
    }

    public List<ProductResponse> GetAllProducts()
    {
        return _productRepository.GetAllProducts();
    }

    public List<GroupTree> GetAllGroups()
    {
        return _productRepository.GetAllGroups();
    }

    public List<GroupTree> BuildGroupsTree(List<GroupTree> allGroups)
    {
        Dictionary<Guid, GroupTree> groupDictionary = new Dictionary<Guid, GroupTree>();

        foreach (var group in allGroups)
        {
            //group doesn't exist
            if (!groupDictionary.ContainsKey(group.GroupId))
            {
                groupDictionary.Add(group.GroupId, group);
            }
            else
            {   //group exists
                var current = groupDictionary[group.GroupId];
                group.ChildGroups = current.ChildGroups;
                groupDictionary.Remove(group.GroupId);
                groupDictionary.Add(group.GroupId, group);
            }

            if (group.ParentGroupId != null)
            {
                //parent group doesn't exist
                if (!groupDictionary.ContainsKey(group.ParentGroupId.Value))
                {
                    groupDictionary.Add(group.ParentGroupId.Value, new GroupTree()
                    {
                        GroupId = group.ParentGroupId.Value, ChildGroups = new List<GroupTree>()
                    });
                }
                
                //parent group exists
                groupDictionary[group.ParentGroupId.Value].ChildGroups.Add(group);
            }
        }

        List<GroupTree> groupTree = new List<GroupTree>();
        foreach (var group in groupDictionary.Values)
        {
            if (group.ParentGroupId == null)
            {
                groupTree.Add(group);
            }
        }

        return groupTree;
    
    }
}