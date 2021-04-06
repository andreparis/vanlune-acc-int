using Accounts.Int.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Int.Application.Application.MediatR.Commands.User.GoogleValidUser
{
    public class GoogleValidUserCommand : IRequest<Response>
    {
        public GoogleLogin GoogleLogin { get; set; }
    }
}
