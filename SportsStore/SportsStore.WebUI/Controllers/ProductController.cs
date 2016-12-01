using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IPorductsRepository repository;
        public int PageSize = 4;

        public ProductController(IPorductsRepository productRepository) {
            this.repository = productRepository;
        }

        public ViewResult List(string category, int page=1) {
            ProductListViewModel model = new ProductListViewModel {
                   Products = repository.Products
                                .Where(p=>category==null || p.Category==category)
                   .OrderBy(p=>p.ProductID)
                   .Skip((page - 1) * PageSize)
                   .Take(PageSize),
                   PagingInfo = new PagingInfo {
                       CurrentPage = page,
                       ItemsPerPage = PageSize,
                       TotalItems = category==null? repository.Products.Count(): 
                       repository.Products.Where(e=> e.Category==category).Count()
                   },
                   CurrencyCategory = category
            };
            return View(model);
        }
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
    }
}