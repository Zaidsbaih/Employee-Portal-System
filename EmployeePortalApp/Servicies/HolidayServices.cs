using AutoMapper;
using EmployeePortalApp.data;
using System.Runtime.Intrinsics.Arm;

namespace EmployeePortalApp.Servicies
{
    public class HolidayServices : IHolidayServices
    {
        EmployeeContext context;
        IMapper mapper;
        public HolidayServices(EmployeeContext _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }
        public void insert(HolidayModel model)
        {
            Holiday holiday = mapper.Map<Holiday>(model);
            context.Holidays.Add(holiday);
            context.SaveChanges();
        }
        public List<HolidayModel> LoadAll()
        {
            List<Holiday> Allholidays = context.Holidays.ToList();
            List<HolidayModel> AllholidaysModel = new List<HolidayModel>();
            AllholidaysModel = mapper.Map<List<HolidayModel>>(Allholidays);
            return AllholidaysModel;

        }
        public void Delete(int id) 
        {
            Holiday holiday = context.Holidays.Find(id);
            
            if (holiday != null)
            {
                context.Holidays.Remove(holiday);
                context.SaveChanges();
            }
        }
        public HolidayModel Load (int id)
        {
            Holiday holiday = context.Holidays.Find(id);
            if (holiday != null)
            {
                HolidayModel model = mapper.Map<HolidayModel>(holiday);
                return model;
            }
            else
                return null;
        }
        public void Update(HolidayModel model)
        {
            Holiday holiday = context.Holidays.Find(model.Id);
            if (holiday != null) 
            {
                holiday.Name = model.Name;
                holiday.StartDate = model.StartDate;
                holiday.EndDate = model.EndDate;
                holiday.Description = model.Description;
                holiday.IsActive = model.IsActive;
                context.SaveChanges();
            }
        }
    }
}
