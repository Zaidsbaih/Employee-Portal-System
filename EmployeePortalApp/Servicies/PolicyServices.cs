using EmployeePortalApp.data;
using EmployeePortalApp.Models;


namespace EmployeePortalApp.Servicies
{
    public class PolicyServices : IPolicyServices
    {
        EmployeeContext context;
        public PolicyServices(EmployeeContext _context) 
        {
            context = _context;
        }
        public void Insert (PolicyModel model)
        {
            Policy policy = new Policy();
            policy.Title = model.Title;
            policy.Category = model.Category;
            policy.Description = model.Description;
            policy.IsActive = model.IsActive;
            policy.UploadDate = DateTime.Now;
            if (model.PdfFile != null)
            {
                string fileName =
                    Guid.NewGuid().ToString()
                    + Path.GetExtension(model.PdfFile.FileName);

                string folderPath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "policies");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string filePath =
                    Path.Combine(folderPath, fileName);

                using (FileStream stream =
                    new FileStream(filePath, FileMode.Create))
                {
                    model.PdfFile.CopyTo(stream);
                }

                policy.FilePath = fileName;
            }
            context.Policy.Add(policy);
            context.SaveChanges();
        }
        public List<PolicyModel> LoadAll()
        {
            return context.Policy
        .OrderByDescending(p => p.UploadDate)
        .Select(p => new PolicyModel
        {
            Id = p.Id,
            Title = p.Title,
            Category = p.Category,
            Description = p.Description,
            FilePath = p.FilePath,
            UploadDate = p.UploadDate,
            IsActive = p.IsActive
        })
        .ToList();
        }

        public PolicyModel Load(int id)
        {
            Policy policy = context.Policy.Find(id);
            if (policy == null)
                return null;
            PolicyModel model = new PolicyModel
            {
                Id = policy.Id,
                Title = policy.Title,
                Category = policy.Category,
                Description = policy.Description,
                FilePath = policy.FilePath,
                UploadDate = policy.UploadDate,
                IsActive = policy.IsActive
            };
            return model;


        }
        public void Update(PolicyModel model)
        {
            Policy? policy =
                context.Policy.Find(model.Id);

            if (policy == null)
                return;

            policy.Title = model.Title;
            policy.Category = model.Category;
            policy.Description = model.Description;
            policy.IsActive = model.IsActive;

            if (model.PdfFile != null)
            {
                string fileName =
                    Guid.NewGuid().ToString()
                    + Path.GetExtension(model.PdfFile.FileName);

                string folderPath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "policies");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string filePath =
                    Path.Combine(folderPath, fileName);

                using (FileStream stream =
                    new FileStream(filePath, FileMode.Create))
                {
                    model.PdfFile.CopyTo(stream);
                }

                policy.FilePath = fileName;
            }

            context.SaveChanges();
        }
        public void Delete(int id)
        {
            Policy? policy =
                context.Policy.Find(id);

            if (policy != null)
            {
                context.Policy.Remove(policy);
                context.SaveChanges();
            }
        }
        public List<PolicyModel> Search(string title)
        {
            IQueryable<Policy> query =
                context.Policy;

            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(p =>
                    p.Title.Contains(title));
            }

            return query
                .Where(p => p.IsActive)
                .OrderByDescending(p => p.UploadDate)
                .Select(p => new PolicyModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Category = p.Category,
                    Description = p.Description,
                    FilePath = p.FilePath,
                    UploadDate = p.UploadDate,
                    IsActive = p.IsActive
                })
                .ToList();
        }
    }
}
