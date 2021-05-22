using DAL;
using DomainModels;
using System.Collections.Generic;

namespace WebApp.XUnitTest.Factories
{
    public static class ProductFactory
    {
        public static List<Product> ProductList { get; }
        public static List<Category> CategoryList { get; }
        public static List<ProductModel> ProductModelList { get; }

        static ProductFactory()
        {        

            if (ProductList == null)
            {
                ProductList = new List<Product>
                {
                    new Product { Id = 1, Name = "MVC Book", CategoryId = 1, Description = "MVC Book", UnitPrice = 199M },
                    new Product { Id = 2, Name = "ASPNET Book", CategoryId = 1, Description = "ASPNET Book", UnitPrice = 299M },
                    new Product { Id = 3, Name = "Core Book", CategoryId = 1, Description = "Core Book", UnitPrice = 399M }
                };
            }
            if (CategoryList == null)
            {
                CategoryList = new List<Category>
                { 
                    new Category { CategoryId = 1, Name = "Books" },
                    new Category { CategoryId = 2, Name = "Courses" }
                };
            }
            if (ProductModelList == null)
            {
                ProductModelList = new List<ProductModel>
                {
                    new ProductModel { ProductId = 1, ProductName = "MVC Book", CategoryId = 1, Description = "MVC Book", UnitPrice = 199M },
                    new ProductModel { ProductId = 2, ProductName = "ASPNET Book", CategoryId = 1, Description = "ASPNET Book", UnitPrice = 299M },
                    new ProductModel { ProductId = 3, ProductName = "Core Book", CategoryId = 1, Description = "Core Book", UnitPrice = 399M }
                };
            }

        }
    }
}
