using EmployeePortalApp.data;
using EmployeePortalApp.Models;
using EmployeePortalApp.servicies;
using EmployeePortalApp.Servicies;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<EmployeeContext>();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddScoped<IEmployeeServicies, EmployeeServicies>();
builder.Services.AddScoped<IDepartmentServices, DepartmentServices>();
builder.Services.AddScoped<IJobTitleServices, JobTitleServices>();
builder.Services.AddScoped<IHolidayServices, HolidayServices>();
builder.Services.AddScoped<IAttendanceServices, AttendanceServices>();
builder.Services.AddScoped<IMyProfileServices, MyProfileServices>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ILeaveServices, LeaveServices>();
builder.Services.AddScoped<IPolicyServices, PolicyServices>();
builder.Services.AddScoped<IHRRequestServices, HRRequestServices>();
builder.Services.AddAutoMapper(config =>
{
    config.AddMaps(AppDomain.CurrentDomain.GetAssemblies());
});
builder.Services.AddSession();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapStaticAssets();

// ضروري لأن HRRequestController يستخدم Attribute Routing
app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Employee}/{action=NewEmployee}/{Id?}")
    .WithStaticAssets();

app.Run();

