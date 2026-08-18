using AutoMapper;
using EmployeePortalApp.data;
using EmployeePortalApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeePortalApp.Servicies
{
    public class MyProfileServices : IMyProfileServices
    {
        EmployeeContext context;
        IMapper mapper;
        public MyProfileServices(EmployeeContext _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }
        public EmployeeModel? GetMyProfile(int employeeId)
        {
            Employee? emp = context.Employees
                .Include(e => e.Department)
                .Include(e => e.JobTitle)
                .Include(e => e.Manager)
                .FirstOrDefault(e => e.Id == employeeId);

            if (emp == null)
                return null;

            EmployeeModel model = mapper.Map<EmployeeModel>(emp);

            model.DepartmentName = emp.Department?.Name;
            model.JobTitleName = emp.JobTitle?.Title;
            model.ManagerName = emp.Manager?.FullName;

            return model;
        }
    }
}
