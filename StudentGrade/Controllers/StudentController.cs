using Microsoft.AspNetCore.Mvc;
using StudentGrade.Interface;

namespace StudentGrade.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // GET: /Student/Index?searchName=...&remarksFilter=...
        public async Task<IActionResult> Index(string searchName, string remarksFilter = "ALL")
        {
            var students = await _studentService.GetStudentsAsync(searchName, remarksFilter);
            ViewBag.SearchName = searchName;
            ViewBag.RemarksFilter = remarksFilter;
            return View(students);
        }

        // POST: /Student/Upsert
        [HttpPost]
        public async Task<IActionResult> Upsert(int StudentId, string StudentName, string SubjectName, int Grade, string Remarks)
        {
            // Find or create subject
            var subjectId = await _studentService.FindOrCreateSubjectAsync(SubjectName);

            if (StudentId == 0)
            {
                // Add
                await _studentService.AddStudentAsync(StudentName, subjectId, Grade);
            }
            else
            {
                // Edit
                var student = await _studentService.GetStudentByIdAsync(StudentId);
                if (student == null)
                {
                    return NotFound();
                }
                student.StudentName = StudentName;
                student.SubjectId = subjectId;
                student.Grade = Grade;
                student.Remarks = Remarks;
                await _studentService.UpdateStudentAsync(student);
            }
            return RedirectToAction("Index");
        }

        // GET: /Student/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            await _studentService.DeleteStudentAsync(id);
            return RedirectToAction("Index");
        }
    }
}