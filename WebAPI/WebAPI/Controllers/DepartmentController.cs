using System;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Domain.Entities;
using WebAPI.UseCases.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : BaseController
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

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            var success = true;
            try
            {
                _depRepository.Delete(id);
            }
            catch (Exception)
            {
                success = false;
            }

            return success
                ? new JsonResult("Delete successful")
                : new JsonResult("Delete was not successful");
        }
    }
}
