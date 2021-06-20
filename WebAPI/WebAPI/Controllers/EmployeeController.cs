using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IWebHostEnvironment _env;

        public EmployeeController(ICrudRepository<Employee> empRepository, IWebHostEnvironment env)
        {
            _empRepository = empRepository;
            _env = env;
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
            var success = true;
            try
            {
                _empRepository.Delete(id);
            }
            catch (Exception)
            {
                success = false;
            }
            return success
                ? new JsonResult("Delete successful")
                : new JsonResult("Delete was not successful");
        }
        
        [HttpPost]
        [Route("UploadPhoto")]
        public JsonResult UploadPhoto()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                var filename = postedFile.FileName;
                var selectPath = _env.ContentRootPath + "/Photos/" + filename;

                using (var stream = new FileStream(selectPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(filename);
            }
            catch (Exception)
            {
                return new JsonResult("anonymous.png");
            }
        }
        
        [Route("GetAllDepartmentNames")]
        public JsonResult GetAllDepartmentNames()
        {
            return new JsonResult(_empRepository.ReadAll());
        }
    }
}