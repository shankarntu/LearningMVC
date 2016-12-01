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
    public class UnitTest1
    {
        /*[TestMethod]
        public void Can_Paginate()
        {
            //Arrange
            Mock<IPorductsRepository> mock = new Mock<IPorductsRepository>();
            mock.Setup(m => m.Products).Returns(
                new Product[] {
                       new Product { ProductID=1,Name="P1"},
                       new Product { ProductID=2,Name="P2"},
                       new Product { ProductID=3,Name="P3"},
                       new Product { ProductID=4,Name="P4"},
                       new Product { ProductID=5,Name="P5"}
                }
                );
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            //Act
            IEnumerable<Product> result = (IEnumerable<Product>)controller.List(null,2).Model;

            //Assert
            Product[] productArray = result.ToArray();
            Assert.IsTrue(productArray.Length == 2);
            Assert.AreEqual(productArray[0].Name, "P4");
            Assert.AreEqual(productArray[1].Name, "P5");
                     
        }

        [TestMethod]
        public void Can_Generate_Page_Links() {
            //Arrange define an HTML helper - we need to do this in order to apply to extension method
            HtmlHelper myHelper = null;

            //Arrange - create PageInfo data
            PagingInfo pagingInfo = new PagingInfo {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            //Arrange - set up the delegate using lambdia expression 
            Func<int, string> pageUrlDelegate = i => "Page" + i;

            //Act
            MvcHtmlString result = myHelper.pageLinks(pagingInfo, pageUrlDelegate);

            //Assert
            Assert.Equals(@"<a class=""btn btn-default""href=Page1"">1</a>"
                        + @"<a class=""btn btn-default btn-primary selected""href=Page2"">2</a>"
                        + @"<a class=""btn btn-default""href=Page3"">3</a>",
                        result.ToString());
        }

        [TestMethod]
        public void Can_Send_Pagination_View_Model() {
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
            ProductListViewModel result = (ProductListViewModel)controller.List(null,2).Model;

            //Assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }*/


        [TestMethod]
        public void Can_Filter_Products()
        {
            //Arrange
            Mock<IPorductsRepository> mock = new Mock<IPorductsRepository>();
            mock.Setup(m => m.Products).Returns(
                new Product[] {
                    new Product { ProductID = 1, Name="P1", Category="Cat1"},
                    new Product { ProductID = 2, Name="P2", Category="Cat2"},
                    new Product { ProductID = 3, Name="P3", Category="Cat1"},
                    new Product { ProductID = 4, Name="P4", Category="Cat2"},
                    new Product { ProductID = 5, Name="P5", Category="Cat3"}
                });

            //Arrange
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            //Action
            Product[] result = ((ProductListViewModel)controller.List("Cat2", 1).Model).Products.ToArray();

            //Act
            //ProductListViewModel result = (ProductListViewModel)controller.List(null, 2).Model;

            //Assert
            Assert.AreEqual(result.Length, 2);
            Assert.IsTrue(result[0].Name == "P2" && result[0].Category == "Cat2");
            Assert.IsTrue(result[1].Name == "P4" && result[1].Category == "Cat2");
        }
    }
}
