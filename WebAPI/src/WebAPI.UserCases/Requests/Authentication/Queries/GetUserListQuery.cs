using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using WebAPI.Entities.Models;
using WebAPI.UserCases.Common.Dto;
using WebAPI.UserCases.Common.Response;

namespace WebAPI.UserCases.Requests.Authentication.Queries
{
    /// <summary>
    /// Sets a property of the request object.
    /// </summary>
    public class GetUserListQuery : IRequest<IEnumerable> {}

    /// <summary>
    /// Implements a handler for the department list request.
    /// </summary>
    public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, IEnumerable>
    {
        private readonly UserManager<User> _userManager;

        public GetUserListQueryHandler(UserManager<User> userManager) =>
            _userManager = userManager;

        /// <summary>
        /// Handles a request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Returns department list.</returns>
        public async Task<IEnumerable> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var userProfiles = new List<ProfileDto>();
                var users = _userManager.Users.ToArray();

                foreach (var user in users)
                {
                    var role = await _userManager.GetRolesAsync(user);

                    userProfiles.Add(new ProfileDto(
                        user.FullName, user.Email, user.UserName, user.DateCreated, role.First()));
                }

                return await Task.FromResult(userProfiles);
            }
            catch (Exception ex)
            {
                return new[]
                {
                    Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message, null))
                };
            }
        }
    }
}