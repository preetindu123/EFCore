using DAL;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        IUnitOfWork _uow;
        public CatalogController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public IEnumerable<ProductModel> GetProducts()
        {
            return _uow.ProductRepo.GetAllProducts(); //OK:200
        }       

        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            return _uow.ProductRepo.FindById(id); //OK:200
        }

        [HttpPost]
        public IActionResult AddProduct(Product model)
        {
            try
            {
                _uow.ProductRepo.Add(model);
                _uow.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, model);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, Product model)
        {
            try
            {
                if (id != model.Id)
                    return BadRequest();

                _uow.ProductRepo.Update(model);
                _uow.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id) //parameter binding
        {
            try
            {
                _uow.ProductRepo.DeleteById(id);
                _uow.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
