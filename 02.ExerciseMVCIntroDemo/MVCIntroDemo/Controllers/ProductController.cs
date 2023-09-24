namespace MVCIntroDemo.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Net.Http.Headers;
    using MVCIntroDemo.Models.Product;
    using System.Text;
    using System.Text.Json;

    public class ProductController : Controller
    {
        public static IEnumerable<ProductViewModel> _products = new List<ProductViewModel>()
        {
            new ProductViewModel()
            {
                Id = Guid.NewGuid(),
                Name = "Cheese",
                Price = 7.00m
            },
            new ProductViewModel()
            {
                Id = Guid.NewGuid(),
                Name = "Ham",
                Price = 5.50m
            },
            new ProductViewModel()
            {
                Id = Guid.NewGuid(),
                Name = "Bread",
                Price = 1.50m
                //Guid.NewGuid()
            }
        };

        [ActionName("My-Products")]
        public IActionResult All(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return View(_products);
            }

            IEnumerable<ProductViewModel> searchProducts = _products
                .Where(p => p.Name.ToLower()
                                  .Contains(keyword.ToLower()))
                .ToArray();

            return View(searchProducts);
        }

        [Route("/Product/Details/{id?}")]
        public IActionResult ById(string id)
        {
            ProductViewModel? product = _products.FirstOrDefault(p => p.Id.ToString().Equals(id));

            if (product == null)
            {
                return this.RedirectToAction("All");
            }
            return this.View(product);
        }

        public IActionResult AllAsJson()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            return Json(_products, options);
        }

        public IActionResult AllAsText()
        {
            var sb = new StringBuilder();

            foreach (var p in _products)
            {
                sb.AppendLine($"Product: {p.Name} - Price: {p.Price} lv.");
            }

            return Content(sb.ToString().TrimEnd());
        }

        public IActionResult DownloadProductsAsTextFile()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var p in _products)
            {
                sb
                  .AppendLine($"#### ID: {p.Id}")
                  .AppendLine($"Product: {p.Name}")
                  .AppendLine($"# Price: {p.Price:f2} lv.")
                  .AppendLine($"_______________________________");
            }

            Response.Headers
                .Add(HeaderNames.ContentDisposition, @"attachment;filename=products.txt");

            return File(Encoding.UTF8.GetBytes(sb.ToString().TrimEnd()), "text/plain");
        }
    }
}