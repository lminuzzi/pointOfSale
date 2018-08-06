using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PointOfSale.Models;
using Microsoft.AspNet.Identity.Owin;

namespace PointOfSale.Controllers
{
    public class OrderController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Order
        public async Task<ActionResult> Index()
        {
            return View(await db.Orders.ToListAsync());
        }

        // GET: Order/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await db.Orders.FindAsync(id);
            order.ItemProduct = await db.ItemProducts.Where(z => z.OrderId == order.OrderId).ToListAsync();
            order.Payment = await db.Payments.FindAsync(order.PaymentId);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "OrderId,UserId,DtOrder,OrderStatus,DtPayment,TotalValue")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(order);
        }

        // POST: Order/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "OrderId,UserId,DtOrder,OrderStatus,DtPayment,Amount")] Order order)
        {
            if (ModelState.IsValid)
            {
                return await this.DoEdit(order);
            }
            return View(order);
        }

        // GET: Order/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await db.Orders.FindAsync(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Order order = await db.Orders.FindAsync(id);
            db.Orders.Remove(order);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        #region OrderStatus Actions
        // GET: Order/Cancel/5
        public async Task<ActionResult> Cancel(int? id)
        {
            return await DoAction(id, OrderStatus.Canceled);
        }

        // GET: Order/Approve/5
        public async Task<ActionResult> Approve(int? id)
        {
            return await DoAction(id, OrderStatus.Approved);
        }

        // GET: Order/Complete/5
        public async Task<ActionResult> Complete(int? id)
        {
            return await DoAction(id, OrderStatus.Complete);
        }

        // GET: Order/Return/5
        public async Task<ActionResult> Return(int? id)
        {
            return await DoAction(id, OrderStatus.Returned);
        }
        #endregion

        #region Shopping Cart Actions
        /// <summary>
        /// Add a product to cart.
        /// </summary>
        /// <param name="id">The identificator of model Product.</param>
        /// <returns>ActionResult</returns>
        // GET: Order/AddCart/5
        public async Task<ActionResult> AddCart(int id)
        {
            Order cart = Session["Cart"] != null ? (Order)Session["Cart"] : new Order();
            var product = db.Products.Find(id);
            if (product != null)
            {
                product.Category = await db.Categories.FindAsync(product.CategoryId);
                var itemProduct = new ItemProduct();
                itemProduct.Product = product;
                itemProduct.ProductId = product.Id;
                itemProduct.Quantity = 1;
                if (HasSessionItem(cart, product))
                {
                    cart.ItemProduct.FirstOrDefault(x => x.Product.Id == product.Id).Quantity += 1;
                }
                else
                {
                    if (cart.ItemProduct == null)
                    {
                        cart.ItemProduct = new List<ItemProduct>();
                    }
                    cart.ItemProduct.Add(itemProduct);
                }
                cart.Amount = cart.ItemProduct.Select(i => i.Product).Sum(d => d.Price * cart.ItemProduct.FirstOrDefault(x => x.Product.Id == d.Id).Quantity);
                cart.ItemProduct = cart.ItemProduct.OrderBy(x => x.Product.Category.Name).ToList<ItemProduct>();
                Session["Cart"] = cart;
            }

            return RedirectToAction("Cart");
        }

        // GET: Order/AddCart/5
        public ActionResult RemoveCart(int id)
        {
            Order cart = Session["Cart"] != null ? (Order)Session["Cart"] : new Order();
            var product = db.Products.Find(id);
            if (product != null)
            {
                var itemProduct = new ItemProduct();
                itemProduct.Product = product;
                if (HasSessionItem(cart, product))
                {
                    cart.ItemProduct.Remove(cart.ItemProduct.FirstOrDefault(x => x.Product.Id == product.Id));
                }
                Session["Cart"] = cart;
            }

            return RedirectToAction("Cart");
        }

        public ActionResult Cart()
        {
            Order cart = Session["Cart"] != null ? (Order)Session["Cart"] : new Order();
            ViewBag.PaymentId = new SelectList(db.Payments, "Id", "Name");
            return View(cart);
        }

        // POST: Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Cart(Order order)
        {
            Order orderNew = this.GetNewOrder(order);
            ApplicationUserManager UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = await UserManager.FindByNameAsync(this.User.Identity.Name);
            orderNew.UserId = user.Id;
            /*
            if (ModelState.IsValid)
            {
            */
                db.Orders.Add(orderNew);
                await db.SaveChangesAsync();
                foreach(ItemProduct itemProduct in order.ItemProduct)
                {
                    itemProduct.OrderId = orderNew.OrderId;
                    db.ItemProducts.Add(itemProduct);
                }
                
                await db.SaveChangesAsync();
                Session["Cart"] = null;
                return RedirectToAction("Index");
            //}

            //return View(order);
        }

        private async Task<ActionResult> DoAction(int? id, OrderStatus orderStatus)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await db.Orders.FindAsync(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            order.OrderStatus = orderStatus;
            if(order.OrderStatus.Equals(OrderStatus.Complete))
            {
                order.DtPayment = DateTime.Now;
            }
            return await this.DoEdit(order);
        }

        private async Task<ActionResult> DoEdit(Order order)
        {
            db.Entry(order).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private Order GetNewOrder(Order order)
        {
            Order orderNew = new Order();
            orderNew.OrderStatus = OrderStatus.InAnalysis;
            orderNew.DtOrder = DateTime.Now;
            //this date will only be taken into account when the order status is completed.
            orderNew.DtPayment = DateTime.Now;
            orderNew.PaymentId = order.PaymentId;
            if (order.Amount == 0)
            {
                orderNew.Amount = this.GetAmount(order);
            }
            else
            {
                orderNew.Amount = order.Amount;
            }
            orderNew.ItemProduct = new List<ItemProduct>();
            return orderNew;
        }

        private Decimal GetAmount(Order order)
        {
            Decimal total = 0;
            foreach(ItemProduct item in order.ItemProduct)
            {
                Product product = db.Products.Find(item.Product.Id);
                total += product.Price * item.Quantity;
            }
            return total;
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private static bool HasSessionItem(Order cart, Product product)
        {
            return cart != null && cart.ItemProduct != null && cart.ItemProduct.FirstOrDefault(x => x.Product.Id == product.Id) != null;
        }
    }
}
