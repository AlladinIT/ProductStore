using Microsoft.AspNetCore.Mvc;
using WebAPI.DTO;
using WebAPI.Services;
using WebAPI.Services.enums;

namespace WebAPI.Controllers;

[Route("api/v1/")]
[Produces("application/json")]
[Consumes("application/json")]
[ApiController]
public class ProductController : ControllerBase
{

    private readonly ProductService _productService;
    
    public ProductController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpPost("products")]
    public IActionResult AddProduct(AddProductRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        
        var res = _productService.AddProduct(request);
        if (res == ServiceResult.GroupNotFound)
        {
            return BadRequest( new { code = 400, error = "No group with id: " + request.GroupId + " exist in db" });
        }

        if (res == ServiceResult.StoreNotFound)
        {
            return BadRequest( new { code = 400, error = "One or more store IDs do not exist in Database." });
        }
        
        return Ok();
    }
    [HttpGet("products")]
    public IActionResult GetProduct(Guid? id)
    {
        
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        if (id.HasValue)
        {
            var product = _productService.GetProduct(id);

            if (product == null)
            {
                return BadRequest( new { code = 400, error = "No product with id: " + id + " exist in db" });
            }
        
            return Ok(product);
        }
        else
        {
            var products = _productService.GetAllProducts();
            return Ok(products);
        }

    }
    
    [HttpGet("groupstree")]
    public IActionResult GetGroupsTree()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        
        List<GroupTree> allGroups = _productService.GetAllGroups();
        List<GroupTree> groupTree = _productService.BuildGroupsTree(allGroups);
        
        return Ok(groupTree);
    }
    
}