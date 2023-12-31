﻿using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.UseCases.Dto;
using WebAPI.UseCases.Requests.Departments.Commands;
using WebAPI.UseCases.Requests.Departments.Queries;
using WebAPI.UseCases.Requests.Departments.Validators;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : BaseController
    {
        /// <summary>
        /// Gets the list of departments.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /department.
        /// </remarks>
        /// <returns>Returns department list.</returns>
        /// <response code="200">Success.</response>
        /// <response code="401">If the user is unauthorized.</response>
        [HttpGet]
        // [Authorize (Roles = "Manager")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable>> GetDepartmentList()
        {
            return Ok(await Mediator.Send(new GetDepartmentListQuery()));
        }

        /// <summary>
        /// Gets the department by id.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /department/D34D349E-43B8-429E-BCA4-793C932FD580.
        /// </remarks>
        /// <param name="id">Department id (guid).</param>
        /// <returns>Returns department dto.</returns>
        /// <response code="200">Success.</response>
        /// <response code="401">If the user in unauthorized.</response>
        [HttpGet("{id}")]
        // [Authorize (Roles = "Manager")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<DepartmentDto>> GetDepartmentById(Guid id)
        {
            var request = new GetDepartmentQuery {Id = id};
            return Ok(await Mediator.Send(request));
        }

        /// <summary>
        /// Creates the department.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /department.
        /// </remarks>
        /// <param name="department">DepartmentDto.</param>
        /// <returns>Returns response about success.</returns>
        /// <response code="201">Success.</response>
        /// <response code="401">If the user is unauthorized.</response>
        [HttpPost]
        //[Authorize (Roles = "Manager")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<string>> CreateDepartment([FromBody] DepartmentDto department)
        {
            var request = new CreateDepartmentCommand() {DepartmentDto = department};
            var validationResult = new CreateDepartmentCommandValidator().Validate(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.First().ErrorMessage);
            }

            return Ok(await Mediator.Send(request));
        }

        /// <summary>
        /// Updates the department.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /department.
        /// </remarks>
        /// <param name="department">DepartmentDto.</param>
        /// <returns>Returns response about success.</returns>
        /// <response code="204">Success.</response>
        /// <response code="401">If the user is unauthorized.</response>
        [HttpPut]
        // [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<string>> UpdateDepartment([FromBody] DepartmentDto employee)
        {
            var request = new UpdateDepartmentCommand() {DepartmentDto = employee};
            var validationResult = new UpdateDepartmentCommandValidator().Validate(request);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.First().ErrorMessage);

            return Ok(await Mediator.Send(request));
        }

        /// <summary>
        /// Deletes the department by id.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /department/88DEB432-062F-43DE-8DCD-8B6EF79073D3.
        /// </remarks>
        /// <param name="id">Id of the department (guid).</param>
        /// <returns>Returns response about success.</returns>
        /// <response code="204">Success.</response>
        /// <response code="401">If the user is unauthorized.</response>
        [HttpDelete("{id}")]
        // [Authorize (Roles = "Manager")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<string>> DeleteDepartmentById(Guid id)
        {
            var request = new DeleteDepartmentCommand {Id = id};
            return Ok(await Mediator.Send(request));
        }
    }
}
