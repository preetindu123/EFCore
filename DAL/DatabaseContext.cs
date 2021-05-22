using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;

namespace DAL
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            var t = Categories;
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        //this doe not work so i had to do it using data annotataions after wch it worked
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Category>().HasKey(x => x.CategoryId);
        //    modelBuilder.Entity<Category>().Property(x => x.Name)
        //                                   .IsRequired()
        //                                   .IsUnicode(false)
        //                                   .HasMaxLength(100);
        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"data source=LAPTOP-OS72J8HC; Initial Catalog=EFCore1; Integrated Security=True;MultipleActiveResultSets=true;");
            }
        }

        public Product usp_getproduct(int ProductId)
        {
            Product product = new Product();
            using (var command = this.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "usp_getproduct";
                command.CommandType = CommandType.StoredProcedure;
                var parameter = new SqlParameter("prouductid", ProductId);                

                command.Parameters.Add(parameter);
                this.Database.OpenConnection();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        product.Id = reader.GetInt32("Id");
                        product.Name = reader.GetString("Name");
                        product.Description = reader.GetString("Description");
                        product.UnitPrice = reader.GetDecimal("UnitPrice");
                        product.CategoryId = reader.GetInt32("CategoryId");
                    }
                }
                this.Database.CloseConnection();
            }
            return product;
        }

        public Product udf_getproduct(int ProductId)
        {
            return Products.FromSqlRaw<Product>("Select * from udf_getproduct(@prouductid)", new SqlParameter("prouductid", ProductId)).FirstOrDefault();
        }
    }
}
