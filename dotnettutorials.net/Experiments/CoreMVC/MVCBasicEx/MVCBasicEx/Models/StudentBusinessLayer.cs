using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBasicEx.Models
{
    public class StudentBusinessLayer
    {
        public IEnumerable<Student> GetAll()
        {
            //logic to return all employees
            throw new NotImplementedException();
        }
        public Student GetById(int StudentID)
        {
            //logic to return an employee by employeeId
            Student student = new Student()
            {
                StudentID = StudentID,
                Name = "James",
                Gender = "Male",
                Branch = "CSE",
                Section = "A2",
            };
            return student;
        }
        public void Insert(Student student)
        {
            //logic to insert an student
        }
        public void Update(Student student)
        {
            //logic to Update an student
        }
        public void Delete(int StudentID)
        {
            //logic to Delete an student
        }
    }
}
