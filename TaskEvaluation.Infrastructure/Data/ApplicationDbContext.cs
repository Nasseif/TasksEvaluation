using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TaskEvaluation.Core.Consts;
using TaskEvaluation.Core.Entities.Business;

namespace TaskEvaluation.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
             : base(options)
        {
        }
    
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<EvaluationGrade> EvalutionGrades { get; set; }
       public DbSet<Group> Groups { get; set; }
        public DbSet<Solution> Solutions { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Assignment)
                .WithOne(a => a.Course)
                .HasForeignKey<Assignment>(a => a.CourseId);

            modelBuilder.Entity<Assignment>()
                .HasOne(a => a.Solution)
                .WithOne(s => s.Assignment)
                .HasForeignKey<Solution>(s => s.AssignmentId);
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);

            
            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                if (foreignKey.DeleteBehavior == DeleteBehavior.SetNull)
                {
                    foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
                }
            }

                modelBuilder.Entity<Course>().HasData(
                new Course { Id = 1, Title = "Front End", IsCompleted = false },
                new Course { Id = 2, Title = "Back End", IsCompleted = false }
            );

            modelBuilder.Entity<Group>().HasData(
                new Group { Id = 1, Title = "A", CourseId = 1 },
                new Group { Id = 2, Title = "B", CourseId = 2 }
            );

            modelBuilder.Entity<EvaluationGrade>().HasData(
                new EvaluationGrade { Id = 1, Grade = "Good" },
                new EvaluationGrade { Id = 2, Grade = "Very good" },
                new EvaluationGrade { Id = 3, Grade = "Excellent" }
            );
        }


        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateAsyncScope())
            {

                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(AppRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(AppRoles.Admin));
                if (!await roleManager.RoleExistsAsync(AppRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(AppRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                string adminUserEmail = "admin@tasks.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        FullName = "Admin User",
                        UserName = "admin-user",
                        Email = adminUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, AppRoles.Admin);
                }


                string appUserEmail = "user@tasks.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new ApplicationUser()
                    {
                        FullName = "Application User",
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, AppRoles.User);
                }
            }
        }

    }

}
