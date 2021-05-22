using DAL;
using DomainModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Repository.UOW;
using System.Collections.Generic;
using WebApp.Controllers;
using WebApp.XUnitTest.Factories;
using Xunit;

namespace WebApp.XUnitTest
{
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

        [Fact]
        public void TestIndex()
        {
            var prod1 = new ProductModel { ProductId = 4, ProductName = "MVVM Book", CategoryId = 1, Description = "MVVM Book", UnitPrice = 199M };
            var prod2 = new ProductModel { ProductId = 5, ProductName = "MVVM Book New", CategoryId = 1, Description = "MVVM Book New", UnitPrice = 199M };
            ProductModelList.Add(prod1);
            uow.Setup(u => u.ProductRepo.GetAllProducts()).Returns(ProductModelList);
            var result = ctrl.Index() as ViewResult;
            var model = result.Model as List<ProductModel>;
            Assert.Equal(4, model.Count);
            Assert.Contains(prod1, model);
            Assert.DoesNotContain(prod2, model);           
        }

        [Fact]
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
            uow.Setup(u => u.ProductRepo.Add(p4)).Callback((Product model) =>
            {
                ProductList.Add(model);
            });
            ctrl.Create(p4);
            Assert.Contains(p4, ProductList);
        }

        [Fact]
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
            Assert.Equal(Id, model.Id);
        }
    }
}
