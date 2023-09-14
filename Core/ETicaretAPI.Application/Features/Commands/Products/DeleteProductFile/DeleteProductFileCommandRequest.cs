using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.Products.DeleteProductFile
{
    public class DeleteProductFileCommandRequest: IRequest<DeleteProductFileCommandResponse>
    {
        public long id { get; set; }
        public long imageid { get; set; }
    }
}
