using ETicaretAPI.Application.Repositories.ProductImageFİle;
using ETicaretAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Repositories.ProductImageFile
{
    public class ProductImageFileWriteRepository : WriteRepository<ETicaretAPI.Domain.Entities.ProductImageFile>, IProductImageFileWriteRepository
    {
        public ProductImageFileWriteRepository(ETicaretAPIDbCoxtext context) : base(context)
        {
        }
    }
}
