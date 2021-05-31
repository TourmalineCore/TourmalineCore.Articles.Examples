using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PaginationExample.Models;
using PaginationExample.Queries;
using TourmalineCore.AspNetCore.Pagination.Extensions;
using TourmalineCore.AspNetCore.Pagination.Models;

namespace PaginationExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductsQuery _productsQuery;

        public ProductsController(ProductsQuery productsQuery)
        {
            _productsQuery = productsQuery;
        }

        [HttpGet("all")]
        public async Task<PaginationResult<ProductDto>> GetProducts()
        {
            var paginationParams = Request.Query.GetPaginationParams();
            return await _productsQuery.GetPageAsync(paginationParams);
        }
    }
}
