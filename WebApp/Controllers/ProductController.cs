using Microsoft.AspNetCore.Mvc;
using System.Linq;
using DAL;
using Repository.UOW;

namespace WebApp.Controllers
{
    public class ProductController : Controller
    {     
        public IUnitOfWork _uow { get; set; }
        public ProductController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public IActionResult Index()
        { 
            var data = _uow.ProductRepo.GetAllProducts();
            return View(data);
        }

        public IActionResult Create()
        {            
            ViewBag.CategoryList = _uow.CategoryRepo.GetAll().ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product model)
        {
            ModelState.Remove("Id");
            if (ModelState.IsValid)
            {                
                _uow.ProductRepo.Add(model);
                _uow.SaveChanges();
                return RedirectToAction("Index");
            }            
            ViewBag.CategoryList = _uow.CategoryRepo.GetAll().ToList();
            return View();
        }
        public IActionResult Edit(int id)
        {
            var product = _uow.ProductRepo.FindById(id);
            ViewBag.CategoryList = _uow.CategoryRepo.GetAll().ToList();
            return View("Create", product);
        }

        [HttpPost]
        public IActionResult Edit(Product model)
        {
            if (ModelState.IsValid)
            {                
                _uow.ProductRepo.Update(model);
                _uow.SaveChanges();
                return RedirectToAction("Index");
            }           
            ViewBag.CategoryList = _uow.CategoryRepo.GetAll().ToList();
            return View("Create", model);
        }

        public IActionResult Delete(int id)
        {            
            var product = _uow.ProductRepo.FindById(id);
            if (product != null)
            {               
                _uow.ProductRepo.DeleteById(id);
                _uow.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(Product model)
        {
            if(ModelState.IsValid)
            {
                _uow.ProductRepo.DeleteById(model.Id);
                _uow.SaveChanges();
            }           
            ViewBag.CategoryList = _uow.CategoryRepo.GetAll().ToList();
            return View("Create", model);
        }
    }

    //public class ProductController : Controller
    //{
    //    //DatabaseContext _dbContext;
    //    //public ProductController(DatabaseContext dbContext)
    //    //{
    //    //    _dbContext = dbContext;
    //    //}

    //    public IUnitOfWork _uow { get; set; }
    //    public ProductController(IUnitOfWork uow)
    //    {
    //        _uow = uow;
    //    }
    //    public IActionResult Index()
    //    {
    //        //var data = _dbContext.Products.ToList();
    //        //var data = _dbContext.Products.Select(x=>x).ToList();
    //        //var data = (from p in _dbContext.Products select p).ToList();

    //        //var data = (from prd in _dbContext.Products
    //        //           join cat in _dbContext.Categories
    //        //           on prd.CategoryId equals cat.CategoryId
    //        //           select new ProductModel
    //        //           {
    //        //               ProductId = prd.Id,
    //        //               ProductName = prd.Name,
    //        //               UnitPrice = prd.UnitPrice,
    //        //               Description = prd.Description,                           
    //        //               CategoryId = cat.CategoryId,
    //        //               CategoryName = cat.Name,
    //        //           }).ToList();

    //        var data = _uow.ProductRepo.GetAllProducts();

    //        return View(data);
    //    }

    //    public IActionResult Create()
    //    {
    //        //ViewBag.CategoryList = _dbContext.Categories.ToList();
    //        ViewBag.CategoryList = _uow.CategoryRepo.GetAll().ToList();
    //        return View();
    //    }

    //    [HttpPost]
    //    public IActionResult Create(Product model)
    //    {
    //        ModelState.Remove("Id");
    //        if(ModelState.IsValid)
    //        {
    //            //_dbContext.Products.Add(model);
    //            //_dbContext.SaveChanges();
    //            _uow.ProductRepo.Add(model);
    //            _uow.SaveChanges();
    //            return RedirectToAction("Index");
    //        }
    //        //ViewBag.CategoryList = _dbContext.Categories.ToList();
    //        ViewBag.CategoryList = _uow.CategoryRepo.GetAll().ToList();
    //        return View();
    //    }
    //    public IActionResult Edit(int id)
    //    {
    //        //var product = _dbContext.Products.Find(id);
    //        //var product = _dbContext.usp_getproduct(id);
    //        //var product = _dbContext.udf_getproduct(id);
    //        //ViewBag.CategoryList = _dbContext.Categories.ToList();

    //        //var product = _dbContext.udf_getproduct(id);
    //        //ViewBag.CategoryList = _dbContext.Categories.ToList();

    //        var product = _uow.ProductRepo.FindById(id);
    //        ViewBag.CategoryList = _uow.CategoryRepo.GetAll().ToList();

    //        return View("Create",product);
    //    }

    //    [HttpPost]
    //    public IActionResult Edit(Product model)
    //    {
    //        if(ModelState.IsValid)
    //        {
    //            //_dbContext.Products.Update(model);
    //            //_dbContext.SaveChanges();
    //            _uow.ProductRepo.Update(model);
    //            _uow.SaveChanges();
    //            return RedirectToAction("Index");
    //        }
    //        //ViewBag.CategoryList = _dbContext.Categories.ToList();
    //        ViewBag.CategoryList = _uow.CategoryRepo.GetAll().ToList();
    //        return View("Create", model);
    //    }

    //    public IActionResult Delete(int id)
    //    {
    //        //var product = _dbContext.Products.Find(id);
    //        var product = _uow.ProductRepo.FindById(id);
    //        if (product != null)
    //        {
    //            //_dbContext.Products.Remove(product);
    //            //_dbContext.SaveChanges();
    //            _uow.ProductRepo.DeleteById(id);
    //            _uow.SaveChanges();
    //        }
    //        return RedirectToAction("Index");

    //    }

    //    [HttpPost]
    //    public IActionResult Delete(Product model)
    //    {
    //        if(ModelState.IsValid)
    //        {
    //            //_dbContext.Products.Remove(model);
    //            //_dbContext.SaveChanges();

    //            _uow.ProductRepo.DeleteById(model.Id);
    //            _uow.SaveChanges();
    //        }
    //        //ViewBag.CategoryList = _dbContext.Categories.ToList();
    //        ViewBag.CategoryList = _uow.CategoryRepo.GetAll().ToList();
    //        return View("Create", model);
    //    }
    //}
}
