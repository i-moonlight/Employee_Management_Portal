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
    }
}