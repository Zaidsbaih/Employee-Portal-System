using EmployeePortalApp.Servicies;
using Microsoft.EntityFrameworkCore;

namespace EmployeePortalApp.data
{
    public class EmployeeContext : DbContext
    {
        public DbSet<HRRequest> HRRequest { get; set; }
        public DbSet<Policy> Policy {  get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Departments> Departments { get; set; }
        public DbSet<Holiday> Holidays { get; set; }
        public DbSet<JobTitle> JobTitles { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=(localdb)\\MSSQLLocalDB;database=EmployeePortalDB;integrated security=true ; TrustServerCertificate = true ");
            base.OnConfiguring(optionsBuilder);
        }

        internal DepartmentServices FindById(int i)
        {
            throw new NotImplementedException();
        }

    }
}
