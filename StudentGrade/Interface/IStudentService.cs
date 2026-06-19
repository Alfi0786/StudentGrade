using StudentGrade.Dto;
using StudentGrade.Models;

namespace StudentGrade.Interface
{
    public interface IStudentService
    {
        Task<List<StudentDisplayDto>> GetStudentsAsync(string searchName = null, string remarksFilter = null);
        Task<int> FindOrCreateSubjectAsync(string subjectName); // If not already present
        Task AddStudentAsync(string studentName, int subjectId, int grade);
        Task<Student> GetStudentByIdAsync(int id);
        Task<List<Subject>> GetAllSubjectsAsync();
        Task DeleteStudentAsync(int id);
        Task UpdateStudentAsync(Student student);
    }
}
