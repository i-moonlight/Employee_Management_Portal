using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using WebAPI.Entities.Models;
using WebAPI.UseCases.Common.Dto.Auth;
using WebAPI.UseCases.Common.Dto.Response;
using WebAPI.Utils.Constants;
using static WebAPI.UseCases.Common.Dto.Response.ResponseCode;

namespace WebAPI.UseCases.Requests.Authentication.Commands
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