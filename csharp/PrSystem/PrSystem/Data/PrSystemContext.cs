using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PrSystem.Models;

namespace PrSystem.Data
{
    public class PrSystemContext : DbContext
    {
        public PrSystemContext (DbContextOptions<PrSystemContext> options)
            : base(options)
        {

        }

        public DbSet<PrSystem.Models.User> Users { get; set; } = default!;
    }
}
