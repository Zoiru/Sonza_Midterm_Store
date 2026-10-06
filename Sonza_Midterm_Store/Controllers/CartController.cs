using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sonza_Midterm_Store.Data;
using Sonza_Midterm_Store.Models;
using System.Linq;

namespace Sonza_Midterm_Store.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CartController(ApplicationDbContext db)
        {
            _db = db;
        }

        // Show cart with live product prices
        public IActionResult Index()
        {
            var items = _db.CartItems
                .Include(c => c.Product) // join with Products table
                .ToList();

            return View(items);
        }

        public IActionResult AddToCart(int id)
        {
            var product = _db.Products.Find(id);
            if (product != null)
            {
                var cartItem = _db.CartItems.FirstOrDefault(c => c.ProductId == id);
                if (cartItem == null)
                {
                    cartItem = new CartItem
                    {
                        ProductId = product.Id,
                        Quantity = 1
                    };
                    _db.CartItems.Add(cartItem);
                }
                else
                {
                    cartItem.Quantity += 1;
                    _db.CartItems.Update(cartItem);
                }
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdateQuantity(int id, int qty)
        {
            var item = _db.CartItems.Find(id);
            if (item != null)
            {
                item.Quantity = qty;
                _db.CartItems.Update(item);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int id)
        {
            var item = _db.CartItems.Find(id);
            if (item != null)
            {
                _db.CartItems.Remove(item);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
