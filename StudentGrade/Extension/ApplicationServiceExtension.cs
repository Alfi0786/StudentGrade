using Microsoft.EntityFrameworkCore;
using StudentGrade.Interface;
using StudentGrade.Models;
using StudentGrade.Repository;
using StudentGrade.Service;

namespace StudentGrade.Extension
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServices
         (this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<GradeAppDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IStudentService, StudentService>();

            return services;
        }
    }
}

