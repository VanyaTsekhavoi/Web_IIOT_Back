using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_IIOT_Back.Models
{
    public class BoxContext : DbContext
    {
        public BoxContext(DbContextOptions<BoxContext> options) : base(options)
        {
        }

        public DbSet<Box> Boxes { get; set; }

    }
}
