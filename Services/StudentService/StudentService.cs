using Azure;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PraticeSessionDemo.Data;
using PraticeSessionDemo.Models;

namespace PraticeSessionDemo.Services.StudentService
{
    public class StudentService : IStudentService
    {
        private DataContext _context;

        public StudentService(DataContext context) 
        {
            _context = context;
        }


        public async Task<List<Student>?> GetAllStudents()
        {
            var students = await _context.Students.ToListAsync();
            return students;
        }
        public  async Task<Student?> GetSigleStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
                return null;
            return student;
        }

        public async Task AddStudent(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return false;
            }
            _context.Students.Remove(student);
            _context.SaveChanges();
            return true;
        }


        public async Task<bool> UpdateStudent(int id, Student studentInfo)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
                return false;

            student.FirstName = studentInfo.FirstName;
            student.LastName = studentInfo.LastName;
            student.Age = studentInfo.Age;

            _context.SaveChanges();

            return true;
        }

        public async Task<bool> UpdateStudentPartially(int id, [FromBody] JsonPatchDocument<Student> partiallyInfo)
        {
            var student = await _context.Students.FindAsync(id);
            if(student == null)
                return false;

            partiallyInfo.ApplyTo(student);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                return false;
            }
            return true;
        }

    }
}
