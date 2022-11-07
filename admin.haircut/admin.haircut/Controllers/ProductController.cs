using Admin.Haircut.Business.Models.Base;
using Admin.Haircut.Business.Service.Interfaces;
using Admin.Haircut.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Haircut.Controllers
{
    [Route("/product")]
    public class ProductController : BaseWebController
    {
        //private readonly IProductService _productService;

        //public ProductController(IProductService productService)
        //{
        //    _productService = productService;
        //}

        [Route("")]
        public async Task<IActionResult> Index(
            )
        {
            

            
            return View();
        }
    }
}
