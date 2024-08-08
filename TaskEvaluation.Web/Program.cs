using Microsoft.EntityFrameworkCore;
using TaskEvaluation.Infrastructure.Data;
using TaskEvaluation.Core.Interfaces.IRepositories;
using TaskEvaluation.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using TaskEvaluation.Web.Services;
using TaskEvaluation.Core.Interfaces.IServices;
using TaskEvaluation.Core.Mapper;
using TaskEvaluation.Infrastructure.Services;
using TaskEvaluation.Infrastructure.Repositoies;
using TaskEvaluation.Core.Consts;
using TaskEvaluation.Core.Entities.Business;

namespace TaskEvaluation.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));

        builder.Services.AddFluentValidationServices();
        builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IEmailSender, EmailSender>();
        builder.Services.AddScoped<IEmailBodyBuilder, EmailBodyBuilder>();

        builder.Services.Configure<MailSettings>(builder.Configuration.GetSection(nameof(MailSettings)));
       
        // Register the services
        builder.Services.AddScoped<ICourseService, CourseService>();
        builder.Services.AddScoped<ISolutionService, SolutionService>(); 
        builder.Services.AddScoped<IStudentService, StudentService>();  
        builder.Services.AddScoped<IAssignmentService, AssignmentService>();    

        // Register AutoMapper
        builder.Services.AddAutoMapper(typeof(MappingProfile)); // Register your mapping profiles

        // Register the custom BaseMapper as a transient service
        builder.Services.AddTransient(typeof(IBaseMapper<,>), typeof(BaseMapper<,>));

        // Authentication and authorization
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders(); // Add default token providers

        builder.Services.AddMemoryCache();
        builder.Services.AddSession();
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        });


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Account}/{action=Login}/{id?}");
        ApplicationDbContext.SeedUsersAndRolesAsync(app).Wait();
        app.Run();
    }
}
