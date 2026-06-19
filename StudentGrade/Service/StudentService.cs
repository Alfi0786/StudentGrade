using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentGrade.Dto;
using StudentGrade.Interface;
using StudentGrade.Models;

namespace StudentGrade.Service
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repository;
       
        public StudentService(IStudentRepository repository)
        {
            _repository = repository;
           
        }

        public async Task<List<StudentDisplayDto>> GetStudentsAsync(string searchName = null, string remarksFilter = null)
        {
            return await _repository.GetStudentsAsync(searchName, remarksFilter);
        }

        public async Task<int> FindOrCreateSubjectAsync(string subjectName)
        {
            return await _repository.FindOrCreateSubjectAsync(subjectName);
        }

        public async Task AddStudentAsync(string studentName, int subjectId, int grade)
        {
            await _repository.AddStudentAsync(studentName, subjectId, grade);
        }

        public async Task<Student> GetStudentByIdAsync(int id)
        {
            return await _repository.GetStudentByIdAsync(id);
        }
        public async Task<List<Subject>> GetAllSubjectsAsync()
        {
            return await _repository.GetAllSubjectsAsync();
        }

        public async Task DeleteStudentAsync(int id)
        {
            await _repository.DeleteStudentAsync(id);
        }

        public async Task UpdateStudentAsync(Student student)
        {
            await _repository.UpdateStudentAsync(student);
        }
    }
}
