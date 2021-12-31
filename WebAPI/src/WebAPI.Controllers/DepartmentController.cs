using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.UserCases.Common.Behaviors;
using WebAPI.UserCases.Common.Dto;
using WebAPI.UserCases.Requests.Departments.Commands.CreateDepartment;
using WebAPI.UserCases.Requests.Departments.Queries.GetDepartment;
using WebAPI.UserCases.Requests.Departments.Queries.GetDepartmentList;

namespace WebAPI.Controllers
{
    [Route("api/department")]
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
            var validationResult = Validation.CreateDepartmentValidator.Validate(request);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.First().ErrorMessage);

            return Ok(await Mediator.Send(request));
        }

        //
        // [HttpPut]
        // public JsonResult Put(Department dep)
        // { 
        //     var success = true;
        //     try
        //     {
        //         _depRepository.Update(dep);
        //     }
        //     catch (Exception)
        //     {
        //         success = false;
        //     }
        //     return success
        //         ? new JsonResult("Update successful")
        //         : new JsonResult("Update was not successful");
        // }
        //
        // [HttpDelete("{id}")]
        // public JsonResult Delete(Guid id)
        // {
        //     var success = true;
        //     try
        //     {
        //         _depRepository.Delete(id);
        //     }
        //     catch (Exception)
        //     {
        //         success = false;
        //     }
        //     return success
        //         ? new JsonResult("Delete successful")
        //         : new JsonResult("Delete was not successful");
        // }
    }
}