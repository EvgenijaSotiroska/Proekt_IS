using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using CoffeeEShop.Domain.DomainModels;

namespace CoffeeEShop.Domain.Identity
{
    public class SystemUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Order? UserOrder { get; set; }
    }
}
