using ETicaretAPI.Application.Exceptions;
using ETicaretAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.AppUsers.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        readonly UserManager<AppUser> _userManager;

        public CreateUserCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {

            var result = await _userManager.CreateAsync(new()
            {
                UserName = request.Email,
                FullName = request.FullName,
                Email = request.Email,
            }, request.Password);
            CreateUserCommandResponse response = new CreateUserCommandResponse() { IsSuccess = result.Succeeded };
            if (result.Succeeded)
                response.Message = "Kullanıcı oluşturuldu";
            else
                response.Message = result.Errors.FirstOrDefault().Description;
            return response;
        }
    }
}
