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
    public class CreateDepartmentCommand : IRequest<string>
    {
        public DepartmentDto DepartmentDto { get; set; }
    }

    /// <summary>
    /// Implements a handler for the department create command.
    /// </summary>
    public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, string>
    {
        private readonly ICrudRepository<Department> _repository;
        private readonly IMapper _mapper;

        public CreateDepartmentCommandHandler(ICrudRepository<Department> repo, IMapper mapper) =>
            (_repository, _mapper) = (repo, mapper);

        /// <summary>
        /// Handles a request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Returns string about success.</returns>
        public async Task<string> Handle(CreateDepartmentCommand request, CancellationToken token)
        {
            var success = true;
            try
            {
                var department = _mapper.Map<Department>(request.DepartmentDto);
                _repository.Create(department);
            }
            catch (Exception)
            {
                success = false;
            }

            return await Task.FromResult(success ? "Created successfully" : "Create failed");
        }
    }
}