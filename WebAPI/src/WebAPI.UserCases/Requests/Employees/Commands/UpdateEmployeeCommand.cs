using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WebAPI.Entities.Models;
using WebAPI.Infrastructure.Interfaces.DataAccess;
using WebAPI.UserCases.Common.Dto;
using static WebAPI.Utils.Constants.MessageTypes;

namespace WebAPI.UserCases.Requests.Employees.Commands
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

        public UpdateEmployeeCommandHandler(ICrudRepository<Employee> repo, IMapper mapper) =>
            (_repository, _mapper) = (repo, mapper);

        /// <summary>
        /// Handles a request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Returns string about success.</returns>
        public async Task<string> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
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

            return await Task.FromResult(success ? UpdatedSuccessfull : UpdatedFailed);
        }
    }
}