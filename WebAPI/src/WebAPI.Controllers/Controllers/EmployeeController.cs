using System;
using System.Threading.Tasks;
using Serilog;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.UserCases.Cases.Employees.Queries.GetEmployee;
using WebAPI.UserCases.Cases.Employees.Queries.GetEmployeeList;
using WebAPI.UserCases.Cases.Employees.Commands.CreateEmployee;
using WebAPI.UserCases.Cases.Employees.Commands.DeleteEmployee;
using WebAPI.UserCases.Cases.Employees.Commands.UpdateEmployee;

namespace WebAPI.Controllers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : BaseController
    {
        private readonly ILogger _logger;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(ILogger logger, IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        /// <summary>
        /// Gets the list of Employee.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /employee.
        /// </remarks>
        /// <returns>Returns EmployeeListViewModel.</returns>
        /// <response code="200">Success.</response>
        /// <response code="401">If the user is unauthorized.</response>
        [HttpGet]
        // [Authorize (Roles = "Manager")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<EmployeeListViewModel>> GetEmployeeList()
        {
            var query = new GetEmployeeListQuery() { EmployeeId = EmployeeId };
            var view = await Mediator.Send(query);
            return Ok(view);
        }
        
        /// <summary>
        /// Gets the employee by id.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /employee/D34D349E-43B8-429E-BCA4-793C932FD580.
        /// </remarks>
        /// <param name="id">Employee id (guid)</param>
        /// <returns>Returns GetEmployeeQuery</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user in unauthorized</response>
        [HttpGet("{id}")]
        // [Authorize (Roles = "Manager")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<GetEmployeeQuery>> GetEmployeeById(Guid id)
        {
            var query = new GetEmployeeQuery { Id = id };
            return Ok(await Mediator.Send(query));
        }
        
        /// <summary>
        /// Creates the employee.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /employee.
        /// </remarks>
        /// <param name="command">CreateEmployeeCommand.</param>
        /// <returns>Returns response about success.</returns>
        /// <response code="201">Success.</response>
        /// <response code="401">If the user is unauthorized.</response>
        [HttpPost]
        //[Authorize (Roles = "Manager")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<string>> CreateEmployee([FromBody] CreateEmployeeCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        
        /// <summary>
        /// Updates the employee.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /employee.
        /// </remarks>
        /// <param name="command">UpdateEmployeeCommand object.</param>
        /// <returns>Returns response about success.</returns>
        /// <response code="204">Success.</response>
        /// <response code="401">If the user is unauthorized.</response>
        [HttpPut]
        // [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<string>> UpdateEmployee([FromBody] UpdateEmployeeCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        
        /// <summary>
        /// Deletes the employee by id.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /employee/88DEB432-062F-43DE-8DCD-8B6EF79073D3.
        /// </remarks>
        /// <param name="id">Id of the employee (guid).</param>
        /// <returns>Returns response about success.</returns>
        /// <response code="204">Success.</response>
        /// <response code="401">If the user is unauthorized.</response>
        [HttpDelete("{id}")]
        // [Authorize (Roles = "Manager")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<string>> DeleteEmployeeById(Guid id)
        {
            var query = new DeleteEmployeeCommand { EmployeeId = id };
            return Ok(await Mediator.Send(query));
        }

        // [HttpPost]
        // [Authorize (Roles = "Manager")]
        // [Route("UploadPhoto")]
        // public JsonResult UploadPhoto()
        // {
        //     try
        //     {
        //         var httpRequest = Request.Form;
        //         var postedFile = httpRequest.Files[0];
        //         var filename = postedFile.FileName;
        //         var selectPath = _env.ContentRootPath + "/Photos/" + filename;
        //
        //         using (var stream = new FileStream(selectPath, FileMode.Create))
        //         {
        //             postedFile.CopyTo(stream);
        //         }
        //
        //         return new JsonResult(filename);
        //     }
        //     catch (Exception)
        //     {
        //         return new JsonResult("anonymous.png");
        //     }
        // }
        //
        // [HttpPost]
        // [Authorize (Roles = "Manager")]
        // [Route("{Id}/UpdatePhoto")]
        // public JsonResult UpdatePhoto(Guid id)
        // {
        //     try
        //     {
        //         var photoName = _empRepository.GetFileName(id);
        //         var httpRequest = Request.Form;
        //         var postedFile = httpRequest.Files[0];
        //         var filename = postedFile.FileName;
        //         var selectPath = _env.ContentRootPath + "/Photos/" + filename;
        //         var storagePath = PathTypes.StoragePath + photoName;
        //
        //         if (System.IO.File.Exists(selectPath))
        //             System.IO.File.Copy(storagePath, selectPath, true);
        //
        //         using (var stream = new FileStream(selectPath, FileMode.Create))
        //         {
        //             postedFile.CopyTo(stream);
        //             if (System.IO.File.Exists(selectPath))
        //                 System.IO.File.Delete(storagePath);
        //         }
        //         return new JsonResult(filename);
        //     }
        //     catch (Exception)
        //     {
        //         return new JsonResult("anonymous.png");
        //     }
        // }
        //
        // [Route("GetAllDepartmentNames")]
        // public JsonResult GetAllDepartmentNames()
        // {
        //     return new JsonResult(_empRepository.ReadAll());
        // }
    }
}