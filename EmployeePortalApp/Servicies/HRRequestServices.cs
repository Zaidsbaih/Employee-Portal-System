using EmployeePortalApp.data;
using EmployeePortalApp.Models;

namespace EmployeePortalApp.Servicies
{
    public class HRRequestServices : IHRRequestServices
    {
        EmployeeContext context;

        public HRRequestServices(
            EmployeeContext _context)
        {
            context = _context;
        }


        // =====================================
        // EMPLOYEE - Submit Request
        // =====================================

        public void SubmitRequest(
            int employeeId,
            HRRequestModel model)
        {
            HRRequest request = new HRRequest
            {
                EmployeeId = employeeId,

                RequestType =
                    model.RequestType,

                Description =
                    model.Description,

                Status = "New",

                RequestDate =
                    DateTime.Now
            };

            context.HRRequest.Add(request);

            context.SaveChanges();
        }



        // =====================================
        // EMPLOYEE - My Requests
        // =====================================

        public HRRequestEmployeeVM GetEmployeeRequests(
            int employeeId)
        {
            HRRequestEmployeeVM vm =
                new HRRequestEmployeeVM();


            vm.NewRequest =
                new HRRequestModel();


            vm.MyRequests =
                context.HRRequest

                .Where(r =>
                    r.EmployeeId == employeeId)

                .OrderByDescending(r =>
                    r.RequestDate)

                .Select(r =>
                    new HRRequestModel
                    {
                        Id = r.Id,

                        EmployeeId =
                            r.EmployeeId,

                        RequestType =
                            r.RequestType,

                        Description =
                            r.Description,

                        Status =
                            r.Status,

                        HRComment =
                            r.HRComment,

                        AttachmentPath =
                            r.AttachmentPath,

                        RequestDate =
                            r.RequestDate,

                        CompletedDate =
                            r.CompletedDate
                    })

                .ToList();


            return vm;
        }



        // =====================================
        // ADMIN - All Requests + Filter
        // =====================================

        public HRRequestAdminVM GetAdminRequests(
            string? employeeName,
            string? requestType,
            string? status)
        {
            // نربط HRRequest مع Employee
            var query =
                from request in context.HRRequest

                join employee in context.Employees

                on request.EmployeeId
                equals employee.Id

                select new
                {
                    Request = request,
                    Employee = employee
                };


            // Search Employee
            if (!string.IsNullOrEmpty(employeeName))
            {
                query = query.Where(x =>
                    x.Employee.FullName
                    .Contains(employeeName));
            }


            // Filter Request Type
            if (!string.IsNullOrEmpty(requestType))
            {
                query = query.Where(x =>
                    x.Request.RequestType
                    == requestType);
            }


            // Filter Status
            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(x =>
                    x.Request.Status
                    == status);
            }


            HRRequestAdminVM vm =
                new HRRequestAdminVM();


            vm.Requests =
                query

                .OrderByDescending(x =>
                    x.Request.RequestDate)

                .Select(x =>
                    new HRRequestModel
                    {
                        Id =
                            x.Request.Id,

                        EmployeeId =
                            x.Request.EmployeeId,

                        EmployeeName =
                            x.Employee.FullName,

                        RequestType =
                            x.Request.RequestType,

                        Description =
                            x.Request.Description,

                        Status =
                            x.Request.Status,

                        HRComment =
                            x.Request.HRComment,

                        AttachmentPath =
                            x.Request.AttachmentPath,

                        RequestDate =
                            x.Request.RequestDate,

                        CompletedDate =
                            x.Request.CompletedDate
                    })

                .ToList();


            // نحافظ على قيم الفلاتر
            vm.EmployeeName =
                employeeName;

            vm.RequestType =
                requestType;

            vm.Status =
                status;


            return vm;
        }



        // =====================================
        // ADMIN - Update Request
        // =====================================

        public void UpdateRequest(
            int requestId,
            string status,
            string? comment,
            IFormFile? attachmentFile)
        {
            HRRequest? request =
                context.HRRequest.Find(requestId);


            if (request == null)
            {
                return;
            }


            // Completed = Closed
            // ما بنسمح تعديله مرة ثانية
            if (request.Status == "Completed")
            {
                return;
            }


            request.Status =
                status;

            request.HRComment =
                comment;



            // =================================
            // Attachment
            // =================================

            if (attachmentFile != null)
            {
                string fileName =
                    Guid.NewGuid().ToString()
                    +
                    Path.GetExtension(
                        attachmentFile.FileName);


                string folderPath =
                    Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "hrrequests");


                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(
                        folderPath);
                }


                string filePath =
                    Path.Combine(
                        folderPath,
                        fileName);


                using (FileStream stream =
                    new FileStream(
                        filePath,
                        FileMode.Create))
                {
                    attachmentFile.CopyTo(stream);
                }


                request.AttachmentPath =
                    fileName;
            }



            // =================================
            // Completed Date
            // =================================

            if (status == "Completed")
            {
                request.CompletedDate =
                    DateTime.Now;
            }


            context.SaveChanges();
        }
    }
}