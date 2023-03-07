using Microsoft.AspNetCore.JsonPatch;
using PraticeSessionDemo.Models;

namespace PraticeSessionDemo.Services.StudentService
{
    public interface IStudentService
    {
        Task<List<Student>?> GetAllStudents();
        Task<Student?> GetSigleStudent(int id);
        Task AddStudent(Student student);
        Task<bool> UpdateStudent(int id, Student student);
        Task<bool> DeleteStudent(int id);
        Task<bool> UpdateStudentPartially (int id, JsonPatchDocument<Student> partially);
    }
}
