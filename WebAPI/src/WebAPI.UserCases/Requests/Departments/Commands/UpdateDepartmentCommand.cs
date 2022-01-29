using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WebAPI.Entities.Models;
using WebAPI.Infrastructure.Interfaces.DataAccess;
using WebAPI.UserCases.Common.Dto;

namespace WebAPI.UserCases.Requests.Departments.Commands
{
    /// <summary>
    /// Sets a property of the command object.
    /// </summary>
    public class UpdateDepartmentCommand : IRequest<string>
    {
        public DepartmentDto DepartmentDto { get; set; }
    }

    /// <summary>
    /// Implements a handler for the department update command.
    /// </summary>
    public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, string>
    {
        private readonly ICrudRepository<Department> _repository;
        private readonly IMapper _mapper;

        public UpdateDepartmentCommandHandler(ICrudRepository<Department> repo, IMapper mapper) =>
            (_repository, _mapper) = (repo, mapper);

        /// <summary>
        /// Handles a request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Returns string about success.</returns>
        public async Task<string> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var success = true;
            try
            {
                var employee = _mapper.Map<Department>(request.DepartmentDto);
                _repository.Update(employee);
            }
            catch (Exception)
            {
                success = false;
            }

            return await Task.FromResult(success ? "Updated successfully" : "Update failed");
        }
    }
}