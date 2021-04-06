using Accounts.Int.Domain.Entities;
using MediatR;

namespace Accounts.Int.Application.Application.MediatR.Commands.User.AuthenticateUser
{
    public class FacebookValidUserCommand : IRequest<Response>
    {
        public FacebookLogin FacebookLogin { get; set; }
    }
}
