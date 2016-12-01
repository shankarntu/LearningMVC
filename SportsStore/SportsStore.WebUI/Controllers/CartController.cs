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
    public class CartController : Controller
    {
        public IPorductsRepository repository;

        public CartController(IPorductsRepository repo)
        {
            repository = repo;
        }

        //Add Product
        public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(a => a.ProductID == productId);

            try
            {
                if (product != null)
                {
                    // GetCart().AddItem(product, 1); Added in Infrastructure/Binders
                   if (cart == null) cart = new Cart();
                    cart.AddItem(product, 1);
                }
            }
            catch (Exception exc)
            {
                string s = exc.Message.ToString();
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        //Remove Product
        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(b => b.ProductID == productId);

            if (product != null)
            {
                //GetCart().RemoveLine(product);Added in Infrastructure/Binders
                cart.RemoveLine(product);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        /*
         * Replace this logic into Infastrcuture/Binders/ModelBinders.cs
         * 
         * public Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];

            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }

            return cart;
        }*/

        // Display Cart Summary
        public ViewResult Index(Cart cart, string returnUrl)
        {
            //return View(new CartIndexModel {
            //    Cart = GetCart(),
            //    ReturnUrl = returnUrl
            //} );

            return View(new CartIndexViewModel
            {
                ReturnUrl = returnUrl,
                Cart = cart
            });
        }
    }
}