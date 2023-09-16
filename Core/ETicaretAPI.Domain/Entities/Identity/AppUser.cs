using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ETicaretAPI.Domain.Entities.Identity
{
    public class AppUser : IdentityUser<long>
    {
        public string FullName { get; set; }
    }
}
