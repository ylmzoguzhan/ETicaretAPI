using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Contexts
{
    public class ETicaretAPIDbCoxtext : DbContext
    {
        public ETicaretAPIDbCoxtext(DbContextOptions options) : base(options)
        {

        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker.Entries<BaseEntity>();
            foreach (var data in datas)
            {
                if(data.State==EntityState.Modified)
                    data.Entity.ModifiedDate = DateTime.Now;
                else if(data.State==EntityState.Added)
                    data.Entity.CreatedDate = DateTime.Now;
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Domain.Entities.File> Files { get; set; }
        public DbSet<ProductImageFile> ProductImageFiles { get; set; }

    }
}
