using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Moq;
using System.Text;
using System.Threading.Tasks;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.Models;
using System.Web.Mvc;

namespace SportsStore.UnitTests.CartTest
{
    [TestClass]
    public class CartTests
    {
       [TestMethod]
        public void Add_New_Lines()
        {
            //Arrnage
            Product P1 = new Product { ProductID = 1, Name = "P1" };
            Product P2 = new Product { ProductID = 2, Name = "P2" };

            //Arrange - new Cart
            SportsStore.Domain.Entities.Cart target = new Domain.Entities.Cart();
            
            //Act
            target.AddItem(P1, 1);
            target.AddItem(P2, 1);
            CartLine[] results = target.Lines.ToArray();

            //ASsert
            Assert.AreEqual(results.Length, 2);
            Assert.AreEqual(results[0].Product, P1);
            Assert.AreEqual(results[1].Product, P2);
        }

        [TestMethod]
        //Combine
        public void Can_Add_Quantity_For_Exisiting_Lines()
        {
            //Arrange
            Product p1 = new Product { ProductID = 1, Name = "p1" };
            Product p2 = new Product { ProductID = 2, Name = "p2" };

            //Act
            SportsStore.Domain.Entities.Cart target = new Domain.Entities.Cart();

            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 10);

            CartLine[] result = target.Lines.OrderBy(a=>a.Product.ProductID).ToArray();

            //Asset
            Assert.AreEqual(result.Length, 2);
            Assert.AreEqual(result[0].Qunatity, 11);
            Assert.AreEqual(result[1].Qunatity, 1);
        }

        [TestMethod]
        public void Add_To_Cart_Seesion()
        {
            //Arrange
            Mock<IPorductsRepository> mock = new Mock<IPorductsRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID=1, Name = "P1", Category="Apples"} }.AsQueryable());

            Cart cart = new Cart();
            CartController target = new CartController(mock.Object);

            //Act
            target.AddToCart(cart, 1, null);

            //Assert
            Assert.AreEqual(cart.Lines.Count(), 1);
            Assert.AreEqual(cart.Lines.ToArray()[0].Product.ProductID, 1);
        }

        [TestMethod]
        public void After_Add_To_Cart_Redirect_To_Screen()
        {
            //Arrange
            Mock<IPorductsRepository> mock = new Mock<IPorductsRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product { ProductID = 1, Name = "P1", Category = "Apples" } }.AsQueryable());

            Cart cart = new Cart();
            CartController target = new CartController(mock.Object);

            //Act
            RedirectToRouteResult result = target.AddToCart(cart, 1, "myUrl");

            //ASsert
            Assert.AreEqual(result.RouteValues["action"], "Index");
            Assert.AreEqual(result.RouteValues["returnUrl"], "myUrl");
        }

        [TestMethod]
        public void Can_Veiw_Cart_Contents()
        {
            //Arrange
            Cart cart = new Cart();
            CartController target = new CartController(null);

            //Act
            CartIndexViewModel result = (CartIndexViewModel)target.Index(cart, "myUrl").ViewData.Model;

            //Assert
            Assert.AreEqual(result.Cart, cart);
            Assert.AreEqual(result.ReturnUrl, "myUrl");
        }
    }
}
