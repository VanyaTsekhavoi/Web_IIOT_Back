using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_IIOT_Back.Models
{
    public class PeripheryContext : DbContext
    {
        public PeripheryContext(DbContextOptions<PeripheryContext> options) : base(options)
        {
        }

        public DbSet<Periphery> Peripheries { get; set; }

    }
}
