using AutoMapper;
using EmployeePortalApp.data;
using EmployeePortalApp.Models;
using EmployeePortalApp.Servicies;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;
using System.Security.Cryptography;
namespace EmployeePortalApp.servicies
{
    public class EmployeeServicies : IEmployeeServicies
    {
        EmployeeContext context;
        IMapper mapper;
        IEmailService emailService;
        IAttendanceServices attendanceServices;
        public EmployeeServicies(EmployeeContext employeeContext, IMapper _mapper, IEmailService _emailService, IAttendanceServices _attendanceServices)
        {
            context = employeeContext;
            mapper = _mapper;
            emailService = _emailService;
            attendanceServices = _attendanceServices;   
        }
        public async Task insert(EmployeeModel Model)
        {
            Employee emp = mapper.Map<Employee>(Model);

            emp.Department = null;
            emp.JobTitle = null;
            emp.Manager = null;

            emp.DepartmentId = Model.DepartmentId;
            emp.JobTitleId = Model.JobTitleId;
            emp.ManagerId = Model.ManagerId;

            if (Model.Photo != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(Model.Photo.FileName);
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    Model.Photo.CopyTo(stream);
                }

                emp.PhotoPath = fileName;
            }
            bool exists = context.Employees.Any(e => e.Email == Model.Email);

            if (exists)
            {
                throw new Exception("Email already exists");
            }
            context.Employees.Add(emp);

            emp.PasswordSetupToken = Convert.ToHexString(RandomNumberGenerator.GetBytes(32));

            emp.PasswordSetupTokenExpiry = DateTime.Now.AddHours(24);
            context.SaveChanges();
            string link =$"https://localhost:44348/Employee/SetPassword?token={emp.PasswordSetupToken}";
            await emailService.SendEmailAsync(emp.Email,
        "Set your password",
    $@"
        <h2>Welcome to Employee Portal</h2>

        <p>Your account has been created.</p>

        <a href='{link}'>
            Click here to set your password
        </a>
    ");
        }
        public List<EmployeeModel> LoadAll()
        {
            return context.Employees
                .Select(emp => new EmployeeModel
                {
                    Id = emp.Id,
                    FullName = emp.FullName,
                    Email = emp.Email,
                    Phone = emp.Phone,
                    JoiningDate = emp.JoiningDate,
                    IsActive = emp.IsActive,

                    DepartmentName = emp.Department.Name,
                    JobTitleName = emp.JobTitle.Title,
                    ManagerName = emp.Manager != null ? emp.Manager.FullName : ""
                })
                .ToList();
        }
        public List<Departments> GetDepartments()
        {
            return context.Departments.ToList();
        }
        public void SetPassword(SetPasswordModel model)
        {
            Employee emp = context.Employees
                .FirstOrDefault(e => e.PasswordSetupToken == model.Token);

            if (emp == null)
                return;

            if (emp.PasswordSetupTokenExpiry < DateTime.Now)
                return;

            emp.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);

            emp.PasswordSetupToken = null;
            emp.PasswordSetupTokenExpiry = null;

            context.SaveChanges();
        }

        public List<JobTitle> GetJobTitles()
        {
            return context.JobTitles.ToList();
        }

        public List<Employee> GetManagers()
        {
            return context.Employees.ToList();
        }


        public void Delete(int id)
        {
            Employee emp = context.Employees.Find(id);
            if (emp != null)
            {
                context.Employees.Remove(emp);
                context.SaveChanges();
            }
        }
        public EmployeeModel Load(int id)
        {
            Employee emp = context.Employees.Find(id);

            if (emp != null)
            {
                EmployeeModel employeeModel = mapper.Map<EmployeeModel>(emp);
                return employeeModel;
            }
            else
                return null;
        }
        public void Update(EmployeeModel model)
        {
            Employee emp = context.Employees.Find(model.Id);
            if (emp != null)
            {
                emp.FullName= model.FullName;
                emp.Email= model.Email;
                emp.DepartmentId= model.DepartmentId;
                emp.Phone= model.Phone;
                emp.JobTitleId= model.JobTitleId;
                emp.IsActive= model.IsActive;
                emp.JoiningDate= model.JoiningDate;
                emp.ManagerId= model.ManagerId;
                emp.BirthDate = model.BirthDate;
                context.SaveChanges();
            }
        }
        public List<EmployeeModel> SearchEmployee(String name,String Phone)
        {
            IQueryable<Employee> query = context.Employees;
            if(! string.IsNullOrEmpty(name))
                query = query.Where(p => p.FullName.Contains(name));
            if (!string.IsNullOrEmpty(Phone))
                query = query.Where(p => p.Phone == Phone);
            return query
       .Select(emp => new EmployeeModel
       {
           Id = emp.Id,
           FullName = emp.FullName,
           Email = emp.Email,
           Phone = emp.Phone,
           JoiningDate = emp.JoiningDate,
           IsActive = emp.IsActive,
           DepartmentName = emp.Department.Name,
           JobTitleName = emp.JobTitle.Title,
           ManagerName = emp.Manager != null ? emp.Manager.FullName : ""})
           .ToList();
        }
        public async Task<bool> ForgotPassword(ForgotPasswordModel model)
        {
            Employee emp = context.Employees
                .FirstOrDefault(e => e.Email == model.Email);

            if (emp == null)
                return false;

            emp.PasswordSetupToken = Convert.ToHexString(RandomNumberGenerator.GetBytes(32));
            emp.PasswordSetupTokenExpiry = DateTime.Now.AddHours(24);

            context.SaveChanges();

            string link =
                $"https://localhost:44348/Employee/SetPassword?token={emp.PasswordSetupToken}";

            await emailService.SendEmailAsync(
                emp.Email,
                "Reset Password",
                $@"
        <h2>Reset Password</h2>
        <p>Click the link below to reset your password.</p>
        <a href='{link}'>Reset Password</a>"
            );

            return true;
        }
        public EmployeeModel? Login(Login_EmpModel model)
        {
            Employee? emp = context.Employees
                .FirstOrDefault(e =>
                    e.Email == model.Email &&
                    e.IsActive);

            if (emp == null || string.IsNullOrEmpty(emp.Password))
                return null;

            bool passwordCorrect =
                BCrypt.Net.BCrypt.Verify(model.Password, emp.Password);

            if (!passwordCorrect)
                return null;

            EmployeeModel employeeModel =mapper.Map<EmployeeModel>(emp);
            return employeeModel;
        }
        public DashboardModel? GetDashboard(int employeeId)
        {

            DateTime today = DateTime.Today;

            Employee? employee = context.Employees
                .Include(e => e.Department)
                .Include(e => e.JobTitle)
                .FirstOrDefault(e => e.Id == employeeId);

            if (employee == null)
                return null;

            DashboardModel dashboardModel = new DashboardModel
            {
                EmployeeId = employee.Id,
                EmployeeName = employee.FullName,
                PhotoPath = employee.PhotoPath,
                DepartmentName = employee.Department?.Name,
                JobTitleName = employee.JobTitle?.Title
            };

            dashboardModel.TodayAttendance =
                attendanceServices.GetTodayAttendance(employeeId);

            // Birthdays
            dashboardModel.TodayBirthdays = context.Employees
                .Where(e =>
                    e.IsActive &&
                    e.BirthDate.HasValue &&
                    e.BirthDate.Value.Month == today.Month &&
                    e.BirthDate.Value.Day == today.Day)
                .Select(e => new EmployeeModel
                {
                    Id = e.Id,
                    FullName = e.FullName,
                    PhotoPath = e.PhotoPath,
                    BirthDate = e.BirthDate,
                    DepartmentName = e.Department.Name,
                    JobTitleName = e.JobTitle.Title
                })
                .ToList();

            // Holidays
            dashboardModel.UpcomingHolidays = context.Holidays
                .Where(h =>
                    h.IsActive &&
                    h.EndDate >= today)
                .OrderBy(h => h.StartDate)
                .Take(5)
                .Select(h => new HolidayModel
                {
                    Id = h.Id,
                    Name = h.Name,
                    StartDate = h.StartDate,
                    EndDate = h.EndDate,
                    Description = h.Description,
                    IsActive = h.IsActive
                })
                .ToList();

            return dashboardModel;
        }
        
    }
}