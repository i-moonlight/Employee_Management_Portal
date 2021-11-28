using System;
using System.Collections;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.UserCases.Cases.Employees.Queries.GetEmployee;
using WebAPI.UserCases.Cases.Employees.Queries.GetEmployeeList;
using WebAPI.UserCases.Cases.Employees.Commands.CreateEmployee;
using WebAPI.UserCases.Cases.Employees.Commands.DeleteEmployee;
using WebAPI.UserCases.Cases.Employees.Commands.UpdateEmployee;
using WebAPI.UserCases.Cases.Employees.Commands.UpdateEmployeePhoto;
using WebAPI.UserCases.Cases.Employees.Commands.UploadEmployeePhoto;
using WebAPI.UserCases.Cases.Employees.Queries.GetDepartmentNameList;
using WebAPI.UserCases.Common.Dto;

namespace WebAPI.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : BaseController
    {
        // private readonly ILogger _logger;
        //
        // public EmployeeController(ILogger logger) => _logger = logger;

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
        /// <param name="id">Employee id (guid)</param>
        /// <returns>Returns GetEmployeeQuery</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user in unauthorized</response>
        [HttpGet("{id}")]
        // [Authorize (Roles = "Manager")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeById(Guid id)
        {
            var query = new GetEmployeeQuery {Id = id};
            var view = await Mediator.Send(query);
            return Ok(view);
        }

        /// <summary>
        /// Gets the list of all department names.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /employee/GetDepartmentNames.
        /// </remarks>
        /// <returns>Returns get all department names.</returns>
        /// <response code="200">Success.</response>
        /// <response code="401">If the user is unauthorized.</response>
        [HttpGet]
        [Route("GetDepartmentNames")]
        // [Authorize(Roles = "Manager")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable>> GetDepartmentNamesList()
        {
            return Ok(await Mediator.Send(new GetDepartmentNameListQuery()));
        }

        /// <summary>
        /// Creates the employee
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
        /// Upload photo the employee by current id.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /employee/UploadPhoto.
        /// </remarks>
        /// <returns>Returns photo file name.</returns>
        /// <response code="204">Success.</response>
        /// <response code="401">If the user is unauthorized.</response>
        [HttpPost]
        [Route("UploadPhoto")]
        // [Authorize (Roles = "Manager")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<string>> UploadEmployeePhoto()
        {
            var query = new UploadPhotoCommand {Id = EmployeeId};
            return Ok(await Mediator.Send(query));
        }

        /// <summary>
        /// Update photo the employee by id.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /employee/UpdatePhoto.
        /// </remarks>
        /// <returns>Returns photo file name.</returns>
        /// <response code="204">Success.</response>
        /// <response code="401">If the user is unauthorized.</response>
        [HttpPost]
        [Route("UpdatePhoto")]
        // [Authorize (Roles = "Manager")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<string>> UpdateEmployeePhoto(Guid id)
        {
            var command = new UpdatePhotoCommand {EmployeeId = id};
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
            var query = new DeleteEmployeeCommand {EmployeeId = id};
            return Ok(await Mediator.Send(query));
        }
    }
}