using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using System.Web.Mvc;
using SportsStore.WebUI.Models;
using SportsStore.WebUI.HtmlHelpers;


namespace SportsStore.UnitTests
{
    [TestClass]
    public class NavControllerTest
    {
        [TestMethod]
        public void Category_By_Null()
        {
            //Arrange
            Mock<IPorductsRepository> mock = new Mock<IPorductsRepository>();
            mock.Setup(m => m.Products).Returns(
                new Product[] {
                    new Product { ProductID = 1, Name="P1"},
                    new Product { ProductID = 2, Name="P2"},
                    new Product { ProductID = 3, Name="P3"},
                    new Product { ProductID = 4, Name="P4"},
                    new Product { ProductID = 5, Name="P5"}
                });

            //Arrange
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            //Act
            ProductListViewModel result = (ProductListViewModel)controller.List(null, 2).Model;

            //Assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }

        [TestMethod]
        public void CategoryFiltering()
        {
            //Arrange
            Mock<IPorductsRepository> mock = new Mock<IPorductsRepository>();
            mock.Setup(m => m.Products).Returns(
                new Product[] {
                    new Product { ProductID = 1, Name="P1",Category="Cat1"},
                    new Product { ProductID = 2, Name="P2",Category="Cat2"},
                    new Product { ProductID = 3, Name="P3",Category="Cat1"},
                    new Product { ProductID = 4, Name="P4",Category="Cat2"},
                    new Product { ProductID = 5, Name="P5",Category="Cat4"}
                });

            //Arrange
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            //Act
            Product[] result = ((ProductListViewModel)controller.List("Cat2", 1).Model)
                                    .Products.ToArray();

            //Assert
            Assert.AreEqual(result.Length, 2);
            Assert.IsTrue(result[0].Name == "P2" && result[0].Category == "Cat2");
            Assert.IsTrue(result[1].Name == "P4" && result[0].Category == "Cat2");
        }

        [TestMethod]
        public void NavigationMenu()
        {
            //Arrange
            Mock<IPorductsRepository> mock = new Mock<IPorductsRepository>();
            mock.Setup(m => m.Products).Returns(
                new Product[] {
                    new Product { ProductID=1, Name="P1", Category="Apples"},
                    new Product { ProductID=2, Name="P2", Category="Apples"},
                    new Product { ProductID=3, Name="P3", Category="Plums"},
                    new Product { ProductID=4, Name="P4", Category="Oranges"}
                });

            NavController target = new NavController(mock.Object);

            //Act
            string[] results = ((IEnumerable<string>)target.Menu().Model).ToArray();

            //Assert
            Assert.AreEqual(results.Length, 3);
            Assert.AreEqual(results[0], "Apples");
            Assert.AreEqual(results[1], "Oranges");
            Assert.AreEqual(results[2], "Plums");
        }
        
    }
}
