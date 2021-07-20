using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectsControl.Models
{
    public class DBProjectContext : DbContext
    {
        internal string MyConnection { get; set; }
        internal IConfiguration configuration { get; set; }

        public DbSet<Asistance> Asistances { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<ExtraHour> ExtraHours { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Saleman> Salemans { get; set; }

        public DBProjectContext(DbContextOptions<DBProjectContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                GetConnectionString();
                options.UseSqlServer(MyConnection);
            }
        }

        private void GetConnectionString(string connectionString = "DBProjectsConnection")
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").AddEnvironmentVariables();
            configuration = builder.Build();
            connectionString = configuration.GetConnectionString(connectionString);
        }
    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            Customer oCustomer = new Customer
            {
                CustomerId = Guid.NewGuid().ToString(),
                Name = "SalemanTesting",
                Sector = "Private"
            };
            Saleman oSaleman = new Saleman
            {
                SalemanId = Guid.NewGuid().ToString(),
                Name = "CustomerTesting"
            };
            Project oProject = new Project
            {
                ProjectId = Guid.NewGuid().ToString(),
                NumberOfTask = 1234,
                Name = "Project Sample",
                OfferId = "1234Sample",
                OC = "1234Sample",
                TypeOfJob="installation",
                IsOver = false,
                Details = "Details sample",
                SalemanId = oSaleman.SalemanId,
                CustomerId = oCustomer.CustomerId
            };
            Employee oEmployee = new Employee
            {
                EmployeeId = Guid.NewGuid().ToString(),
                Name = "Sample of Employee",
                Position = "Sample"

            };
            Asistance oAsistance = new Asistance
            {
                AsistanceId = Guid.NewGuid().ToString(),
                EmployeeId = oEmployee.EmployeeId
            };
            ExtraHour oExtra = new ExtraHour
            {
                ExtraHourId = Guid.NewGuid().ToString(),
                BeginTime = DateTime.Now,
                EndTime = DateTime.Now,
                Reason = "SAMPLE",
                IsPaid = true,
                TypeOfHour="double",
                EmployeeId = oEmployee.EmployeeId,
                AsistanceId = oAsistance.AsistanceId
            };
            
            modelBuilder.Entity<Saleman>().HasData(oSaleman);
            modelBuilder.Entity<Customer>().HasData(oCustomer);
            modelBuilder.Entity<Employee>().HasData(oEmployee);
            modelBuilder.Entity<Project>().HasData(oProject);
            modelBuilder.Entity<Asistance>().HasData(oAsistance);
            modelBuilder.Entity<ExtraHour>().HasData(oExtra);
        }
    }
}
