using StudentGrade.Dto;
using StudentGrade.Models;

namespace StudentGrade.Interface
{
    public interface IStudentRepository
    {
        Task<List<StudentDisplayDto>> GetStudentsAsync(string searchName = null,string remarksFilter = null);
        Task AddStudentAsync(string studentName, int subjectId, int grade);
        Task<int> FindOrCreateSubjectAsync(string subjectName);
        Task<Student> GetStudentByIdAsync(int id);
        Task<List<Subject>> GetAllSubjectsAsync();
        Task DeleteStudentAsync(int id);
        Task UpdateStudentAsync(Student student);
    }
}
