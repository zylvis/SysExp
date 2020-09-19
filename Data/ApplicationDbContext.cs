using SysExp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Charting.Data
{
    public class ApplicationDbContext: IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        
        public DbSet<Category> Category { get; set; }
        public DbSet<Strategy> Strategy { get; set; }
        public DbSet<Portfolio> Portfolio { get; set; }
        public DbSet<ChartData> ChartData { get; set; }
        public DbSet<Description> Description{ get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }

    }
}
