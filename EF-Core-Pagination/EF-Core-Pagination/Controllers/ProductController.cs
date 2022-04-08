using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EF_Core_Pagination.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly DataContext _context;

        public ProductController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{page}")]
        //[Route("{page}")]
        // I put the route attribute here to show that instead of adding the Route attribute we can just add the route to the HttpGet attribute
        public async Task<ActionResult<ProductResponse>> GetProducts(int page)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }

            var pageResults = 3f;
            var pageCount = Math.Ceiling(_context.Products.Count() / pageResults);

            if (page > pageCount)
            {
                return NotFound();
            }

            var products = await _context.Products
                .Skip((page - 1) * (int)pageResults)
                .Take((int)pageResults)
                .ToListAsync();

            var response = new ProductResponse
            {
                Products = products,
                CurrentPage = page,
                Pages = (int)pageCount
            };

            return Ok(response);
        }
    }
}
