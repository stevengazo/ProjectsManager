using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ProjectsControl.Models;

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
            Customer oCustomer = new Customer{CustomerId = Guid.NewGuid().ToString(),Name = "SalemanTesting",Sector = "Private"};
            Saleman oSaleman = new Saleman{SalemanId = Guid.NewGuid().ToString(),Name = "CustomerTesting"};
            Week oWeek = new Week{WeekId = Guid.NewGuid().ToString(), NumberOfWeek="1-2011'",BeginOfWeek = DateTime.Now,EndOfWeek = DateTime.Now,};
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
                    EmployeeId = oEmployee.EmployeeId,
                    WeekId= oWeek.WeekId                
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
                    AsistanceId = oAsistance.AsistanceId,
                    WeekId= oWeek.WeekId
                };
            Bill oBill = new Bill { BillId = Guid.NewGuid().ToString(),
                                    NumberOfBill = "1234AVF",
                                    DateOfCreation= DateTime.Now,
                                    Author="Sample Author",
                                    cost= 123.12f,
                                    ProjectId = oProject.ProjectId
            };
            Report oReport = new Report {
                                    ReportId = Guid.NewGuid().ToString(),
                                    NumberOfReport = 0001,
                                    Status = "Everything is God...",
                                    ProjectId= oProject.ProjectId
            };
            Contact oContact = new Contact {
                                    ContactId= Guid.NewGuid().ToString(),
                                    Name = "Contact Sample",
                                    PhoneNumber = 88888888,
                                    Email="Sample@sample.com",
                                    Position="Manager sample",
                                    CustomerId= oCustomer.CustomerId
            };

            
            modelBuilder.Entity<Saleman>().HasData(oSaleman);
            modelBuilder.Entity<Week>().HasData(oWeek);
            modelBuilder.Entity<Customer>().HasData(oCustomer);
            modelBuilder.Entity<Employee>().HasData(oEmployee);
            modelBuilder.Entity<Project>().HasData(oProject);
            modelBuilder.Entity<Asistance>().HasData(oAsistance);
            modelBuilder.Entity<ExtraHour>().HasData(oExtra);
            modelBuilder.Entity<Contact>().HasData(oContact);
        }
    
        public DbSet<ProjectsControl.Models.Bill> Bill { get; set; }
    
        public DbSet<ProjectsControl.Models.Contact> Contact { get; set; }
    
        public DbSet<ProjectsControl.Models.Report> Report { get; set; }
    
        public DbSet<ProjectsControl.Models.Week> Week { get; set; }
    }
}
