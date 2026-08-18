using EmployeePortalApp.Servicies;
using Microsoft.AspNetCore.Mvc;

namespace EmployeePortalApp.Controllers
{
    public class HolidayController : Controller
    {
        IHolidayServices holidayServices;
        public HolidayController(IHolidayServices _holidayServices)
        {
            holidayServices = _holidayServices;
        }
        public IActionResult NewHoliday()
        {
            ViewBag.IsEdit = false;
            return View();
        }

        public IActionResult SaveHoliday(HolidayModel model)
        {
            holidayServices.insert(model);
            return View("NewHoliday");
        }
        public IActionResult HolidayList()
        {
            List<HolidayModel> holidayList = holidayServices.LoadAll();
            return View(holidayList);
        }
        public IActionResult DeleteHoliday(int id)
        {
            holidayServices.Delete(id);
            return RedirectToAction("HolidayList");
        }
        public IActionResult EditHoliday(int id) 
        {
            ViewBag.IsEdit = true;
            HolidayModel model  = holidayServices.Load(id);
            return View("NewHoliday", model);
        }
        public IActionResult UpdateHoliday(HolidayModel model)
        {
            holidayServices.Update(model);
            return RedirectToAction("HolidayList");
        }
    }
}
