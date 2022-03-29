using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using WebAPI.Service.Authentication.Entities;
using WebAPI.Service.Authentication.UseCases.Constants;
using WebAPI.Service.Authentication.UseCases.Dto;
using static WebAPI.Service.Authentication.UseCases.Dto.ResponseCode;

namespace WebAPI.Service.Authentication.UseCases.Commands
{
    /// <summary>
    /// Sets a property of the request object.
    /// </summary>
    public class RegisterUserCommand : IRequest<ResponseModel>
    {
        public RegisterUserDto RegisterUserDto { get; set; }
    }

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ResponseModel>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public RegisterUserCommandHandler(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles a request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Returns string about success.</returns>
        public async Task<ResponseModel> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = _mapper.Map<User>(request.RegisterUserDto);
                var result = await _userManager.CreateAsync(user, request.RegisterUserDto.Password);

                if (result.Succeeded)
                {
                    var tempUser = await _userManager.FindByEmailAsync(request.RegisterUserDto.Email);

                    await _userManager.AddToRoleAsync(tempUser, RoleNameTypes.AllRoles.ElementAt(0));

                    return await Task.FromResult(new ResponseModel(Ok, MessageTypes.RegistrationSuccess, ""));
                }

                return await Task.FromResult(new ResponseModel(Ok, MessageTypes.RegistrationFailed,
                    result.Errors.Select(error => error.Description).ToArray()));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(Error, ex.Message, ""));
            }
        }
    }
}