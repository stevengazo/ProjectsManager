using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectsControl.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            IdentityRole _AdminRole = new() {
                Id = Guid.NewGuid().ToString(),
                Name= "Admin",
                NormalizedName = "ADMIN"
            };
            IdentityRole _Lector = new()
            {
                Id= Guid.NewGuid().ToString(),
                Name= "Lector",
                NormalizedName ="Lector"
            };
            IdentityRole _Editor = new() { 
                Id= Guid.NewGuid().ToString(),
                Name="Editor",
                NormalizedName= "EDITOR"
            };

            builder.Entity<IdentityRole>().HasData(_AdminRole);
            builder.Entity<IdentityRole>().HasData(_Editor);
            builder.Entity<IdentityRole>().HasData(_Lector);
        }
    }
}
