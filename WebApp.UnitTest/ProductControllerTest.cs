using DAL;
using DomainModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Repository.UOW;
using System.Collections.Generic;
using WebApp.Controllers;
using WebApp.UnitTest.Factories;

namespace WebApp.UnitTest
{
    [TestClass]
    public class ProductControllerTest
    {
        Mock<IUnitOfWork> uow;
        ProductController ctrl;
        public List<Product> ProductList { get; }
        public List<ProductModel> ProductModelList { get; }
        public List<Category> CategoryList { get; }

        public ProductControllerTest()
        {
            ProductList = ProductFactory.ProductList;
            ProductModelList = ProductFactory.ProductModelList;
            CategoryList = ProductFactory.CategoryList;
            uow = new Mock<IUnitOfWork>();
            ctrl = new ProductController(uow.Object);
        }

        [TestMethod]
        public void TestIndex()
        {
            var prod1 = new ProductModel { ProductId = 4, ProductName = "MVVM Book", CategoryId = 1, Description = "MVVM Book", UnitPrice = 199M };            
            var prod2 = new ProductModel { ProductId = 5, ProductName = "MVVM Book New", CategoryId = 1, Description = "MVVM Book New", UnitPrice = 199M };
            ProductModelList.Add(prod1);
            uow.Setup(u => u.ProductRepo.GetAllProducts()).Returns(ProductModelList);
            var result = ctrl.Index() as ViewResult;
            var model = result.Model as List<ProductModel>;
            Assert.AreEqual(model.Count, 4);
            CollectionAssert.Contains(model, prod1);
            CollectionAssert.DoesNotContain(model, prod2);
        }

        [TestMethod]
        public void TestCreatePost()
        {
            Product p4 = new Product { Id = 4, Name = "MVVM Book", CategoryId = 1,
                                        Description = "MVVM Book", UnitPrice = 399M };
            uow.Setup(u => u.ProductRepo.Add(p4)).Callback((Product model) =>
            {
                ProductList.Add(model);
            });
            ctrl.Create(p4);
            CollectionAssert.Contains(ProductList, p4);
        }

        [TestMethod]
        public void TestEditGet()
        {
            int Id = 1;         
            uow.Setup(u => u.ProductRepo.FindById(Id)).Returns((object id) =>
            {
                return ProductList.Find(p => p.Id == (int)id);
            });
            uow.Setup(u => u.CategoryRepo.GetAll()).Returns(CategoryList);            
            var result = ctrl.Edit(Id) as ViewResult;
            var model = result.Model as Product;            
            Assert.AreEqual(Id, model.Id);
        }
    }
}
