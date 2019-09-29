using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class ApiDBContext : DbContext
    {
        public ApiDBContext(DbContextOptions<ApiDBContext> options)
            :base(options)
        {;
            Database.Migrate();
            Database.EnsureCreated();
        }

        public virtual DbSet<Ppg> ppg { get; set; }

        public virtual DbSet<Temp> temp { get; set; }




    }
}
