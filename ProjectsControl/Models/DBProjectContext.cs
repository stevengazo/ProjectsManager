
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
        internal IConfiguration Configuration { get; set; }

        public DbSet<Action> Actions { get; set; }
        public DbSet<Asistance> Asistances { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Expensive> Expensives { get; set; }
        public DbSet<ExtraHour> ExtraHours { get; set; }
        public DbSet<Notes> Notes { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Project> Projects { get; set; }        
        public DbSet<Report> Reports { get; set; }


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
            Configuration = builder.Build();
            MyConnection = Configuration.GetConnectionString(connectionString);
        }
    

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            Customer ocustomer = new()
            {
                CustomerId = Guid.NewGuid().ToString(),
                DNIOfCustomer = 0110,
                Name = "Sample",
                Sector="Private"
            };
            Offer oOffer = new()
            {
                OfferId = Guid.NewGuid().ToString(),
                NumberOfOffer = 1,
                Title = "Title Sample",
                Type="installation",
                Description = "sample of description",
                Author = "Sample of Author",
                SaleManName = "Sample of name",
                DateOfCreation = DateTime.Today,
                Amount = 100.3f,
                LastEdition = DateTime.Today,
                CustomerId = ocustomer.CustomerId,        
            };
            Employee Oemployee = new()
            {
                EmployeeId = Guid.NewGuid().ToString(),
                EmployeeDNI = 1171292,
                Name = "Sample of name",
                DateofHiring = DateTime.Today,
                DateOfBirth = DateTime.Today,
                DateOfFired = DateTime.Today,
                IsActive = true,
                Position = "d",
                MobileNumber = 888,
                Email = "sample@grupomecsa.net",                
            };
            Salary OSalary = new() {
                EmployeeId = Oemployee.EmployeeId,
                SalaryId = Guid.NewGuid().ToString(),
                SalaryAmount = 100,
                notes = "Sample of salary",
                DayOfApplication = DateTime.Today,
                isActive = false
            };
            Project oProject = new()
            {
                ProjectId = Guid.NewGuid().ToString(),
                NumberOfProject = 1,
                NumberOfTask = 1,
                ProjectName = "Sample Of Project",
                OC = "Oc Id Sample",
                OCDate = DateTime.Today,
                BeginDate = DateTime.Today,
                EndDate = DateTime.Today,
                Manager = "Sample of Name",
                Technician = "Sample",
                Estatus = "In progress",
                IsOver = false,
                Amount = 100f,
                Currency = "Dolar",
                PendingAmount = 0,
                TypeOfJob = "Sample of Job",
                Details = "Sample of details",
                Ubication = "San JoseCosta Rica",
                NumberOfOffer = "PS1",
                CustomerId= ocustomer.CustomerId,
                EmployeeId= Oemployee.EmployeeId,
                OfferId = oOffer.OfferId
            };
            Bill obills = new()
            {
                BillId = Guid.NewGuid().ToString(),
                NumberOfBill = 1,
                DateOfCreation= DateTime.Today,
                Author = "Sample",
                Currency="Dolar",
                Amount=1,
                Notes="Sample of notes",
                ProjectId= oProject.ProjectId
            };
            Expensive oexpensive = new() {
                ExpensiveId = Guid.NewGuid().ToString(),
                Author= "Sample Of authot",
                LastModification= DateTime.Today,
                Type="Km Cost",
                Amount = 1.12f,
                Currency="Dolar",
                Note="Sample",
                ProjectId= oProject.ProjectId
            };
            Notes oNotes = new(){
                NotesId = Guid.NewGuid().ToString(),
                Author= "Sample",
                DateOfCreation= DateTime.Today,
                Title= "Sample", 
                NoteDescription= "Description of the action",
                ProjectId= oProject.ProjectId,
            };
            Report oReport = new() {
                ReportId = Guid.NewGuid().ToString(),
                NumberOfReport = 1,
                Author = "Sample of author",
                BeginDate = DateTime.Today,
                EndDate = DateTime.Today,
                Status = "sample of estatus",
                Notes = "sample of notes",
                ProjectId= oProject.ProjectId
            };
            Action oaction = new() {
                ActionId = Guid.NewGuid().ToString(),
                Title = "Sample of title",
                DateOfCreation= DateTime.Today,
                Author="Sample of author",
                TypeOfAction="sample of type",
                Description="sample of description",
                IsActive = true,
                EmployeeId= Oemployee.EmployeeId
            };
            Asistance oAsistance = new() {
                AsistanceId = Guid.NewGuid().ToString(),
                DateOfBegin = DateTime.Today,
                DateOfEnd = DateTime.Today,
                EmployeeId = Oemployee.EmployeeId,
                NumberOfWeek = "01",
                ProjectId= oProject.ProjectId
            };
            ExtraHour oextra = new() {
                ExtraHourId = Guid.NewGuid().ToString(),
                BeginTime = DateTime.Today,
                EndTime = DateTime.Today,
                TypeOfHour = "double",
                Reason = "ad",
                Notes = "as",
                IsPaid = false,
                AceptedBy = "Nyree",
                EmployeeId = Oemployee.EmployeeId,
                AsistanceId= oAsistance.AsistanceId,
                NumberOfWeek = "01"
            };



            modelBuilder.Entity<Employee>().HasData(Oemployee);
            modelBuilder.Entity<Customer>().HasData(ocustomer);
            modelBuilder.Entity<Offer>().HasData(oOffer);            
            modelBuilder.Entity<Project>().HasData(oProject);
            modelBuilder.Entity<Notes>().HasData(oNotes);
            modelBuilder.Entity<Report>().HasData(oReport);
            modelBuilder.Entity<Expensive>().HasData(oexpensive);
            modelBuilder.Entity<Bill>().HasData(obills);
            modelBuilder.Entity<Asistance>().HasData(oAsistance);
            modelBuilder.Entity<ExtraHour>().HasData(oextra);
            modelBuilder.Entity<Action>().HasData(oaction);

        }
    
        public DbSet<ProjectsControl.Models.Bill> Bill { get; set; }
                
        public DbSet<ProjectsControl.Models.Report> Report { get; set; }
   
    
        public DbSet<ProjectsControl.Models.Salary> Salary { get; set; }
    }
}
