using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.Products.CreateProductFile
{
    public class CreateProductFileCommandRequest :IRequest<CreateProductFileCommandResponse>
    {
        public long id { get; set; }
        public IFormFileCollection? Files { get; set; }
    }
}
