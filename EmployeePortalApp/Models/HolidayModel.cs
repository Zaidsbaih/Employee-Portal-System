using AutoMapper;
using EmployeePortalApp.data;

[AutoMap(typeof(Holiday), ReverseMap = true)]
public class HolidayModel
{
    public int Id { get; set; }

    public string Name { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string? Description { get; set; }

    public bool IsActive { get; set; }
}