using ETicaretAPI.Application.Repositories.ProductImageFİle;
using ETicaretAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Repositories.ProductImageFile
{
    public class ProductImageFileReadRepository : ReadRepository<ETicaretAPI.Domain.Entities.ProductImageFile>, IProductImageFileReadRepository
    {
        public ProductImageFileReadRepository(ETicaretAPIDbCoxtext context) : base(context)
        {
        }
    }
}
