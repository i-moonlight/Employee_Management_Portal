using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Domain.Entities;
using WebAPI.Helpers;
using WebAPI.UseCases.Dto;
using WebAPI.UseCases.Requests.Employees.Commands;
using WebAPI.UseCases.Requests.Employees.Queries;
using WebAPI.UseCases.Requests.Employees.Validators;
using WebAPI.UseCases.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : BaseController
    {
        private readonly ICrudRepository<Employee> _empRepository;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(ICrudRepository<Employee> repo, IWebHostEnvironment env)
        {
            _empRepository = repo;
            _env = env;
        }

        /// <summary>
        /// Gets the list of Employee.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /employee.
        /// </remarks>
        /// <returns>Returns employee list.</returns>
        /// <response code="200">Success.</response>
        /// <response code="401">If the user is unauthorized.</response>
        [HttpGet]
        // [Authorize (Roles = "Manager")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable>> GetEmployeeList()
        {
            return Ok(await Mediator.Send(new GetEmployeeListQuery()));
        }

        /// <summary>
        /// Gets the employee by id.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /employee/D34D349E-43B8-429E-BCA4-793C932FD580.
        /// </remarks>
        /// <param name="id">Employee id (guid).</param>
        /// <returns>Returns employee dto.</returns>
        /// <response code="200">Success.</response>
        /// <response code="401">If the user in unauthorized.</response>
        [HttpGet("{id}")]
        // [Authorize (Roles = "Manager")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeById(Guid id)
        {
            var request = new GetEmployeeQuery {Id = id};
            return Ok(await Mediator.Send(request));
        }

        /// <summary>
        /// Creates the employee.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /employee.
        /// </remarks>
        /// <param name="employee">EmployeeDto.</param>
        /// <returns>Returns response about success.</returns>
        /// <response code="201">Success.</response>
        /// <response code="401">If the user is unauthorized.</response>
        [HttpPost]
        //[Authorize (Roles = "Manager")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<string>> CreateEmployee([FromBody] EmployeeDto employee)
        {
            var request = new CreateEmployeeCommand() {EmployeeDto = employee};
            var validationResult = new CreateEmployeeCommandValidator().Validate(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.First().ErrorMessage);
            }

            return Ok(await Mediator.Send(request));
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
        public async Task<ActionResult<string>> UpdateEmployee([FromBody] EmployeeDto employee)
        {
            var request = new UpdateEmployeeCommand() {EmployeeDto = employee};
            var validationResult = new UpdateEmployeeCommandValidator().Validate(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.First().ErrorMessage);
            }

            return Ok(await Mediator.Send(request));
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
            var request = new DeleteEmployeeCommand {Id = id};
            return Ok(await Mediator.Send(request));
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

        [HttpPost]
        [Route("{Id}/UpdatePhoto")]
        public JsonResult UpdatePhoto(int id)
        {
            try
            {
                var photoName = _empRepository.GetFileName(id);
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                var filename = postedFile.FileName;
                var selectPath = _env.ContentRootPath + "/Photos/" + filename;
                var storagePath = Constants.StoragePath + photoName;

                if (System.IO.File.Exists(selectPath)) System.IO.File.Copy(storagePath, selectPath, true);

                using (var stream = new FileStream(selectPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                    if (System.IO.File.Exists(selectPath)) System.IO.File.Delete(storagePath);
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
