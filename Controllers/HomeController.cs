using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StockManagentSystem.Models;
using System.Diagnostics;

namespace StockManagentSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly StockManagementContext _context;
        private readonly IWebHostEnvironment _env;

        public HomeController(StockManagementContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index(int? categoryId)
        {
            List<Product> products;
            if (categoryId.HasValue)
            {
                products = _context.Products.Where(p => p.CategoryId == categoryId).ToList();
            }
            else
            {
                products = _context.Products.ToList();
            }

            var p = products.Select(e => new ProductEdit
            {
                ProId = e.ProId,
                ProName = e.ProName,
                ProImage = e.ProImage,
                Stock = e.Stock,
                Description = e.Description,
                BrandName = e.BrandName,
                ProPrice = e.ProPrice,
                CategoryId = e.CategoryId
            }).ToList();

            ViewData["Productss"] = products;
            return View(p);
        }


        public IActionResult CreateProduct()
        {
            var categories = _context.Categories.ToList();

            var model = new ProductEdit
            {
                Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.CatId.ToString(),
                    Text = c.CatName
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult CreateProduct(ProductEdit p)
        {
            short maxid;
            if (_context.Products.Any())
                maxid = Convert.ToInt16(_context.Products.Max(x => x.ProId) + 1);
            else
                maxid = 1;
            p.ProId = maxid;

            if (p.ProductFile != null)
            {
                string fileName = "ProductImage" + Guid.NewGuid() + Path.GetExtension(p.ProductFile.FileName);
                string filePath = Path.Combine(_env.WebRootPath, "ProductImage", fileName);
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    p.ProductFile.CopyTo(stream);
                }
                p.ProImage = fileName;
            }

            p.CategoryId = p.FetchCategoryId;

            Product product = new()
            {
                ProId = p.ProId,
                ProName = p.ProName,
                ProPrice = p.ProPrice,
                ProImage = p.ProImage,
                Description = p.Description,
                Stock = p.Stock,
                BrandName = p.BrandName,
                CategoryId = p.CategoryId
            };
            _context.Add(product);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            Product? product = _context.Products.Where(e => e.ProId == id).FirstOrDefault();

            if (product == null)
            {
                return NotFound();
            }


            ProductEdit productss = new()
            {
                ProId = product.ProId,
                ProName = product.ProName,
                ProPrice = product.ProPrice,
                Stock = product.Stock,
                Description = product.Description,
                BrandName = product.BrandName,
                ProImage = product.ProImage,
                CategoryId = product.CategoryId

              
            };
            return View(productss);


        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductEdit p)
        {
            var product = await _context.Products.Where(e => e.ProId == p.ProId).FirstAsync();


            if (p.ProductFile != null)
            {
                string fileName = "ProductImage" + Guid.NewGuid() + Path.GetExtension(p.ProductFile.FileName);
                string filePath = Path.Combine(_env.WebRootPath, "ProductImage", fileName);
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    p.ProductFile.CopyTo(stream);
                }
                p.ProImage = fileName;
            }

            product.ProName = p.ProName;
            product.ProPrice = p.ProPrice;
            product.Stock = p.Stock;
            product.Description = p.Description;
            product.BrandName = p.BrandName;
            product.ProImage = p.ProImage;
            product.ProPrice = p.ProPrice;
            product.Stock = p.Stock;
            product.CategoryId = product.CategoryId;

         

            _context.Products.Update(product);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        // delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
          //  return Json(id);
            var u = _context.Products.Where(x => x.ProId == id).FirstOrDefault();
            
            _context.Products.Remove(u);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
