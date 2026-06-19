using Microsoft.EntityFrameworkCore;
using StudentGrade.Dto;
using StudentGrade.Interface;
using StudentGrade.Models;

namespace StudentGrade.Repository
{
    public class StudentRepository: IStudentRepository
    {
        private readonly GradeAppDbContext _context;

        public StudentRepository(GradeAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<StudentDisplayDto>> GetStudentsAsync(string searchName = null, string remarksFilter = null)
        {
            var query = _context.Students.Include(s => s.Subject).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchName))
                query = query.Where(s => s.StudentName.Contains(searchName));

            if (!string.IsNullOrEmpty(remarksFilter) && remarksFilter != "ALL")
                query = query.Where(s => s.Remarks == remarksFilter);

            var students = await query
                .Select(s => new StudentDisplayDto
                {
                    StudentId = s.StudentId,
                    StudentName = s.StudentName,
                    SubjectName = s.Subject.SubjectName,
                    Grade = s.Grade,
                    Remarks = s.Remarks
                })
                .ToListAsync();

            return students;
        }
        public async Task<int> FindOrCreateSubjectAsync(string subjectName)
        {
            var subject = await _context.Subjects
                .FirstOrDefaultAsync(s => s.SubjectName == subjectName);

            if (subject != null)
                return subject.SubjectId;

            subject = new Subject { SubjectName = subjectName };
            _context.Subjects.Add(subject);
            await _context.SaveChangesAsync();
            return subject.SubjectId;
        }

        private string GetRemarksForGrade(int grade)
        {
            return grade >= 75 ? "Passed" : "Failed";
        }

        public async Task AddStudentAsync(string studentName, int subjectId, int grade)
        {
            var student = new Student
            {
                StudentName = studentName,
                SubjectId = subjectId,
                Grade = grade,
                Remarks = GetRemarksForGrade(grade)
            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync();
        }

        public async Task<Student> GetStudentByIdAsync(int id)
        {
            return await _context.Students.FindAsync(id);
        }

        public async Task DeleteStudentAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Subject>> GetAllSubjectsAsync()
        {
            return await _context.Subjects.ToListAsync();
        }

        public async Task UpdateStudentAsync(Student student)
        {
            var existing = await _context.Students.FindAsync(student.StudentId);
            if (existing != null)
            {
                existing.StudentName = student.StudentName;
                existing.SubjectId = student.SubjectId;
                existing.Grade = student.Grade;
                existing.Remarks = GetRemarksForGrade(student.Grade); // Always recalculate
                await _context.SaveChangesAsync();
            }
        }
    }
}
