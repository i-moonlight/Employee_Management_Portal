using System;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ICrudRepository<Employee> _empRepository;

        public EmployeeController(ICrudRepository<Employee> empRepository)
        {
            _empRepository = empRepository;
        }

        [HttpGet]
        public JsonResult Get()
        {
            return new JsonResult(_empRepository.Read());
        }
        
        [HttpPost]
        public JsonResult Post(Employee emp)
        {
            _empRepository.Create(emp);
            return new JsonResult("Created Successfully");
        }

        [HttpPut]
        public JsonResult Put(Employee emp)
        {
            var success = true;
            try
            {
                _empRepository.Update(emp);
            }
            catch (Exception)
            {
                success = false;
            }
            return success
                ? new JsonResult("Update successful")
                : new JsonResult("Update was not successful");
        }
        
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            _empRepository.Delete(id);
            return new JsonResult("Delete successful");
        }
    }
}