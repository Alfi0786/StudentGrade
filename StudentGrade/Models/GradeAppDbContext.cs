using Microsoft.EntityFrameworkCore;

namespace StudentGrade.Models
{
    public class GradeAppDbContext: DbContext
    {
        public GradeAppDbContext(DbContextOptions<GradeAppDbContext> options) : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
    
    }
}
