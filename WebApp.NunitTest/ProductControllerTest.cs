using DAL;
using DomainModels;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Repository.UOW;
using System.Linq;
using System.Collections.Generic;
using WebApp.Controllers;
using WebApp.NUnitTest.Factories;

namespace WebApp.NunitTest
{
    public class Tests
    {
        IUnitOfWork uow;
        ProductController ctrl;
        public List<Product> ProductList { get; internal set; }
        public List<ProductModel> ProductModelList { get; internal set; }
        public List<Category> CategoryList { get; internal set; }
        [SetUp]
        public void Setup()
        {
            Initialize();
        }

        private void Initialize()
        {
            ProductList = ProductFactory.ProductList;
            ProductModelList = ProductFactory.ProductModelList;
            CategoryList = ProductFactory.CategoryList;
            uow = A.Fake<IUnitOfWork>();
            ctrl = new ProductController(uow);
        }      

        [Test]
        public void TestIndex()
        {
            var prod1 = new ProductModel { ProductId = 4, ProductName = "MVVM Book", CategoryId = 1, Description = "MVVM Book", UnitPrice = 199M };
            var prod2 = new ProductModel { ProductId = 5, ProductName = "MVVM Book New", CategoryId = 1, Description = "MVVM Book New", UnitPrice = 199M };
            ProductModelList.Add(prod1);
            A.CallTo(() => uow.ProductRepo.GetAllProducts()).Returns(ProductModelList);
            var result = ctrl.Index() as ViewResult;
            var model = result.Model as List<ProductModel>;
            Assert.AreEqual(model.Count, 4);
            CollectionAssert.Contains(model, prod1);
            CollectionAssert.DoesNotContain(model, prod2);
        }

        [Test]
        public void TestCreatePost()
        {
            Product p4 = new Product
            {
                Id = 4,
                Name = "MVVM Book",
                CategoryId = 1,
                Description = "MVVM Book",
                UnitPrice = 399M
            };
            A.CallTo(() => uow.ProductRepo.Add(p4))
             .Invokes(() => ProductList.Add(p4));          
            ctrl.Create(p4);
            CollectionAssert.Contains(ProductList, p4);
        }

        [Test]
        public void TestEditGet()
        {
            int Id = 1;
            var product = ProductList.FirstOrDefault(x => x.Id == Id);
            A.CallTo(() => uow.ProductRepo.FindById(Id))
           .Returns(product);
            A.CallTo(() => uow.CategoryRepo.GetAll()).Returns(CategoryList);
            var result = ctrl.Edit(Id) as ViewResult;
            var model = result.Model as Product;
            Assert.AreEqual(Id, model.Id);
        }
    }
}