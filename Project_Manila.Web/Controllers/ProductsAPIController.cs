using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Project_Manila.DAL;
using Project_Manila.DAL.Models;

namespace Project_Manila.Web.Controllers
{
    public class ProductsAPIController : Controller
    {
        private readonly ProjectManilaDBContext _context;
        private const string _baseUrl = "http://localhost:36648/api/Products";

        public ProductsAPIController(ProjectManilaDBContext context)
        {
            _context = context;
        }

        // GET: ProductsAPI
        public async Task<IActionResult> Index()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(_baseUrl);

            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var products = JsonConvert.DeserializeObject<List<Product>>(await response.Content.ReadAsStringAsync()).AsEnumerable();
            return View(products);
        }

        // GET: ProductsAPI/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }

            var client = new HttpClient();
            var response = await client.GetAsync($"{_baseUrl}/{id.Value}");

            if (response.IsSuccessStatusCode)
            {
                var product = JsonConvert.DeserializeObject<Product>(await response.Content.ReadAsStringAsync());

                return View(product);
            }

            return NotFound();
        }

        // GET: ProductsAPI/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductsAPI/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,Description,CurrentPrice")] Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            try
            {
                var client = new HttpClient();
                string json = JsonConvert.SerializeObject(product);
                var response = await client.PostAsync(_baseUrl, new StringContent(json, Encoding.UTF8, "application/json"));
                
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Unable to create record: {ex.Message}");
            }
            return View(product);
        }

        // GET: ProductsAPI/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }

            var client = new HttpClient();
            var response = await client.GetAsync($"{_baseUrl}/{id.Value}");

            if (response.IsSuccessStatusCode)
            {
                var product = JsonConvert.DeserializeObject<Product>(
                    await response.Content.ReadAsStringAsync());

                return View(product);
            }
            return new NotFoundResult();
        }

        // POST: ProductsAPI/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,Description,CurrentPrice")] Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            var client = new HttpClient();
            string json = JsonConvert.SerializeObject(product);
            var response = await client.PutAsync($"{_baseUrl}/{product.ProductId}", new StringContent(json, Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: ProductsAPI/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }

            var client = new HttpClient();
            var response = await client.GetAsync($"{_baseUrl}/{id.Value}");

            if (response.IsSuccessStatusCode)
            {
                var product = JsonConvert.DeserializeObject<Product>(await response.Content.ReadAsStringAsync());

                return View(product);
            }

            return new NotFoundResult();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete([Bind("ProductId")] Product product)
        {
            try
            {
                var client = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, $"{_baseUrl}/{product.ProductId}")
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json")
                    };

                var response = await client.SendAsync(request);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Unable to delete record: {ex.Message}");
            }

            return View(product);
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
