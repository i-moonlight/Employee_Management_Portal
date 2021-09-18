using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;

using WebAPI.Domain.Entities;
using WebAPI.UseCases.Dto;
using WebAPI.UseCases.Services;
using WebAPI.Utils.Constants;

namespace WebAPI.UseCases.Requests.Employees.Commands
{
    /// <summary>
    /// Sets a property of the command object.
    /// </summary>
    public class UpdateEmployeeCommand : IRequest<string>
    {
        public EmployeeDto EmployeeDto { get; set; }
    }

    /// <summary>
    /// Implements a handler for the employee update command.
    /// </summary>
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, string>
    {
        private readonly ICrudRepository<Employee> _repository;
        private readonly IMapper _mapper;

        public UpdateEmployeeCommandHandler(ICrudRepository<Employee> repo, IMapper mapper)
        {
            (_repository, _mapper) = (repo, mapper);
        }

        /// <summary>
        /// Handles a request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Returns string about success.</returns>
        public async Task<string> Handle(UpdateEmployeeCommand request, CancellationToken token)
        {
            var success = true;
            try
            {
                var employee = _mapper.Map<Employee>(request.EmployeeDto);
                _repository.Update(employee);
            }
            catch (Exception)
            {
                success = false;
            }

            return await Task.FromResult(success ? ReportTypes.UpdatedSuccessfull : ReportTypes.UpdatedFailed);
        }
    }
}
