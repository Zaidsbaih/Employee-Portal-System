namespace EmployeePortalApp.Servicies
{
    public interface IHolidayServices
    {
        void insert(HolidayModel model);
        List<HolidayModel> LoadAll();

        void Delete(int id);
        HolidayModel Load(int id);
        void Update(HolidayModel model);

    }
}
