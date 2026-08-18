using AutoMapper;
using EmployeePortalApp.data;
using EmployeePortalApp.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NuGet.Protocol;

namespace EmployeePortalApp.Servicies
{
    public class DepartmentServices : IDepartmentServices
    {
        IMapper mapper;
        EmployeeContext employeeContext;
        public DepartmentServices(IMapper _mapper, EmployeeContext _employeeContext)
        {
            mapper = _mapper;
            employeeContext = _employeeContext;
        }
        public void insert(DepartmentsModel model)
        {
            Departments newDepartment = mapper.Map<Departments>(model);

            employeeContext.Departments.Add(newDepartment);
            employeeContext.SaveChanges();
        }

        public List<DepartmentsModel> LoadAll()
        {
            List<Departments> allDepartment = employeeContext.Departments.ToList();
            List<DepartmentsModel> allDepartmentModel = new List<DepartmentsModel>();
            allDepartmentModel = mapper.Map<List<DepartmentsModel>>(allDepartment);
            return allDepartmentModel;

        }
        public void Delete(int id)
        {
            Departments dep = employeeContext.Departments.Find(id);
            if (dep != null)
            {
                employeeContext.Departments.Remove(dep);
                employeeContext.SaveChanges();
            }

        }
        public DepartmentsModel Load (int id)
        {
            Departments dep = employeeContext.Departments.Find(id);
            if (dep != null)
            {
             DepartmentsModel dep1 =mapper.Map<DepartmentsModel>(dep);
                return dep1;
            }
            else
            {
                return null;
            }
        }
        public void Update(DepartmentsModel model)
        {
            Departments dep = employeeContext.Departments.Find(model.Id);
            if (dep != null)
            {
                dep.Name = model.Name;
                employeeContext.SaveChanges();
            }
        }
    }
}
