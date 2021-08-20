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

        public DbSet<Action> Actions { get; set; }
        public DbSet<Asistance> Asistances { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Expensive> Expensives { get; set; }
        public DbSet<ExtraHour> ExtraHours { get; set; }
        public DbSet<Notes> Notes { get; set; }
        public DbSet<Of_Quo> Of_Quos { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Quotation> Quotations { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Week> Weeks { get; set; }


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

            Week oweek = new Week() { WeekId = "01-2010",
                BeginOfWeek = DateTime.Today,
                EndOfWeek = DateTime.Today,
            };
            Quotation oquotation = new Quotation { 
                QuotationId="001-2010",
                Type="Sample",
                Description="Sample Of Description",                
            };
            Customer ocustomer = new Customer
            {
                CustomerId = Guid.NewGuid().ToString(),
                DNIOfCustomer = 0110,
                Name = "Sample",
                Sector="Private"
            };
            Offer oOffer = new Offer {
                OfferId = Guid.NewGuid().ToString(),
                NumberOfOffer = 1,
                Title = "Title Sample",
                Type="installation",
                Description = "sample of description",
                Author = "Sample of Author",
                SaleManName = "Sample of name",
                DateOfCreation = DateTime.Today,
                LastEdition = DateTime.Today,
                CustomerId = ocustomer.CustomerId,        
            };
            Of_Quo oOf_Quo = new Of_Quo()
            {
                Of_QuoId = Guid.NewGuid().ToString(),
                OfferId = oOffer.OfferId,
                QuotationId = oquotation.QuotationId,

            };
            Project oProject = new Project()
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
                Estatus="In progress",
                IsOver = false,
                Amount= 100f,
                Currency= "Dolar",
                PendingAmount= 0,
                TypeOfJob="Sample of Job",
                Details="Sample of details",
                Ubication= "San JoseCosta Rica", 
                CustomerId= ocustomer.CustomerId,
                OfferId= oOffer.OfferId,
            };
            Bill obills = new Bill {
                BillId = Guid.NewGuid().ToString(),
                NumberOfBill = 1,
                DateOfCreation= DateTime.Today,
                Author = "Sample",
                Currency="Dolar",
                Amount=1,
                Notes="Sample of notes",
                ProjectId= oProject.ProjectId
            };
            Expensive oexpensive = new Expensive() {
                ExpensiveId = Guid.NewGuid().ToString(),
                Author= "Sample Of authot",
                LastModification= DateTime.Today,
                Type="Km Cost",
                Amount = 1.12f,
                Currency="Dolar",
                Note="Sample",
                ProjectId= oProject.ProjectId
            };
            Notes oNotes = new Notes {
                NotesId = Guid.NewGuid().ToString(),
                Author= "Sample",
                DateOfCreation= DateTime.Today,
                Title= "Sample", 
                NoteDescription= "Description of the action",
                ProjectId= oProject.ProjectId,
            };
            Report oReport = new Report() {
                ReportId = Guid.NewGuid().ToString(),
                NumberOfReport = "01-2010",
                Author = "Sample of author",
                BeginDate = DateTime.Today,
                EndDate = DateTime.Today,
                Status = "sample of estatus",
                Notes = "sample of notes",
                ProjectId= oProject.ProjectId
            };
            Employee Oemployee = new Employee()
            {
                EmployeeId = Guid.NewGuid().ToString(),
                EmployeeDNI = 1171292,
                Name = "Sample of name",
                DateofHiring = DateTime.Today,
                DateOfBirth = DateTime.Today,
                DateOfFired = DateTime.Today,
                IsActive = true,
                Position="d",
                MobileNumber = 888,
                Email = "sample@grupomecsa.net",
                Salary = 100
            };
            Action oaction = new Action {
                ActionId = Guid.NewGuid().ToString(),
                Title = "Sample of title",
                DateOfCreation= DateTime.Today,
                Author="Sample of author",
                TypeOfAction="sample of type",
                Description="sample of description",
                IsActive = true,
                EmployeeId= Oemployee.EmployeeId
            };
            Asistance oAsistance = new Asistance() { 
                AsistanceId= Guid.NewGuid().ToString(),
                DateOfBegin= DateTime.Today,
                DateOfEnd = DateTime.Today,
                EmployeeId = Oemployee.EmployeeId,
                WeekId = oweek.WeekId,
                ProjectId= oProject.ProjectId
            };
            ExtraHour oextra = new ExtraHour() { 
                ExtraHourId= Guid.NewGuid().ToString(),
                BeginTime= DateTime.Today,
                EndTime= DateTime.Today,
                TypeOfHour= "double",
                Reason= "ad",
                Notes="as",
                IsPaid=false,
                AceptedBy="Nyree",
                AsistanceId= oAsistance.AsistanceId,
                WeekId= oweek.WeekId
            };



            modelBuilder.Entity<Employee>().HasData(Oemployee);
            modelBuilder.Entity<Week>().HasData(oweek);
            modelBuilder.Entity<Customer>().HasData(ocustomer);
            modelBuilder.Entity<Quotation>().HasData(oquotation);
            modelBuilder.Entity<Offer>().HasData(oOffer);
            modelBuilder.Entity<Of_Quo>().HasData(oOf_Quo);
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
    
        public DbSet<ProjectsControl.Models.Week> Week { get; set; }
    }
}
