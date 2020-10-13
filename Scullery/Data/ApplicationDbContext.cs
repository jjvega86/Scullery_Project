using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Scullery.Models;

namespace Scullery.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Planner> Planners { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);
        //    builder.Entity<IdentityRole>()
        //    .HasData(
        //    new IdentityRole
        //    {
        //        Name = "Planner",
        //        NormalizedName = "PLANNER"
        //    }
        //    );
        //}
    }
}
