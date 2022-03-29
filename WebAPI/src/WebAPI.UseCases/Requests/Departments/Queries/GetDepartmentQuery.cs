using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WebAPI.Entities;
using WebAPI.Infrastructure.Interfaces.DataAccess;
using WebAPI.UseCases.Common.Dto;
using WebAPI.UseCases.Common.Exceptions;

namespace WebAPI.UseCases.Requests.Departments.Queries
{
    /// <summary>
    /// Sets a property of the request object.
    /// </summary>
    public class GetDepartmentQuery : IRequest<DepartmentDto>
    {
        public Guid Id { get; set; }
    }
    
    /// <summary>
    /// Implements a handler for the department request.
    /// </summary>
    public class GetDepartmentQueryHandler : IRequestHandler<GetDepartmentQuery, DepartmentDto>
    {
        private readonly ICrudRepository<Department> _repository;
        private readonly IMapper _mapper;

        public GetDepartmentQueryHandler(ICrudRepository<Department> repo, IMapper mapper) =>
            (_repository, _mapper) = (repo, mapper);

        /// <summary>
        /// Handles a request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Returns department by id.</returns>
        public async Task<DepartmentDto> Handle(GetDepartmentQuery request, CancellationToken cancellationToken)
        {
            var department = _repository.Read(request.Id);

            if (department == null || department.Id != request.Id)
                throw new NotFoundException(nameof(Department), request.Id);

            var departmentDto = _mapper.Map<DepartmentDto>(department);

            return await Task.FromResult(departmentDto);
        }
    }
}