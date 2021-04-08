using System;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly ICrudRepository<Department> _depRepository;

        public DepartmentController(ICrudRepository<Department> depRepository)
        {
            _depRepository = depRepository;
        }

        [HttpGet]
        public JsonResult Get()
        {
            return new JsonResult(_depRepository.Read());
        }
        
        [HttpPost]
        public JsonResult Post(Department dep)
        {
            _depRepository.Create(dep);
            return new JsonResult("Created Successfully");
        }
        
        [HttpPut]
        public JsonResult Put(Department dep)
        { 
            var success = true;
            try
            {
                _depRepository.Update(dep);
            }
            catch (Exception)
            {
                success = false;
            }
            return success
                ? new JsonResult("Update successful")
                : new JsonResult("Update was not successful");
        }
    }
}