using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using WebAPI.Service.Authentication.Entities;
using WebAPI.Service.Authentication.UseCases.Dto;

namespace WebAPI.Service.Authentication.UseCases.Commands
{
    /// <summary>
    /// Sets a property of the request object.
    /// </summary>
    public class GetUserListQuery : IRequest<IEnumerable> {}

    /// <summary>
    /// Implements a request handler for a list of registered users.
    /// </summary>
    public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, IEnumerable>
    {
        private readonly UserManager<User> _userManager;

        public GetUserListQueryHandler(UserManager<User> userManager) => _userManager = userManager;

        /// <summary>
        /// Handles a request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Returns registration user list.</returns>
        public async Task<IEnumerable> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            var userProfiles = new List<ProfileDto>();
            
            var users = _userManager.Users;

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                userProfiles.Add(new ProfileDto(user.FullName, user.Email, user.UserName, user.DateCreated,
                    roles.FirstOrDefault()));
            }

            return await Task.FromResult(userProfiles);
        }
    }
}