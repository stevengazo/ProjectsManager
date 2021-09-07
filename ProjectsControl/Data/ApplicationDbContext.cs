using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
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
            var AdministratorRole = new IdentityRole()
            {
                Name = "Administrador",
                NormalizedName = "Administrador".ToUpper()
            };
            var EditorRole = new IdentityRole()
            {
                Name = "Editor",
                NormalizedName = "Editor".ToUpper()
            };


            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>().HasData(EditorRole);
        }
    }
}
