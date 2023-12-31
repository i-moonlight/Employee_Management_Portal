﻿using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.UseCases.Dto;
using WebAPI.UseCases.Requests.Employees.Commands;
using WebAPI.UseCases.Requests.Employees.Queries;
using WebAPI.UseCases.Requests.Employees.Validators;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : BaseController
    {
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
        [Route("DepartmentNames")]
        // [Authorize(Roles = "Manager")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable>> GetDepartmentNameList()
        {
            return Ok(await Mediator.Send(new GetDepartmentNameListQuery()));
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
        /// Upload photo the employee.
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
            return Ok(await Mediator.Send(new UploadPhotoCommand()));
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
            var request = new UpdatePhotoCommand {Id = id};
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
    }
}
