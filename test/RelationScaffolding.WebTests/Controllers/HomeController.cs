using RelationScaffolding.WebTests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RelationScaffolding.WebTests.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Read()
        {
            return View(GetViewModel());
        }

        // GET: Home
        public ActionResult Write()
        {
            return View(GetViewModel());
        }

        private ViewModel GetViewModel()
        {
            return new ViewModel
            {
                Teacher = GetTeacher(),
                AvailableCourses = GetAvailableCourses(),
                Course = GetCourse(),
                AvailableTeachers = GetAvailableTeachers(),
                Student = GetStudent()
            };
        }

        private Student GetStudent()
        {
            var id = 1;
            return new Student
            {
                Id = id,
                Name = "Student 1",
                Enrollments = new List<Enrollment>
                {
                    new Enrollment
                    {
                        Identification = 1,
                        CourseId = 1,
                        Course = GetAvailableCourses().First(m => m.Id == 1),
                        StudentId = id
                    },
                    new Enrollment
                    {
                        Identification = 2,
                        CourseId = 2,
                        Course = GetAvailableCourses().First(m => m.Id == 2),
                        StudentId = id
                    }
                }
            };
        }

        /*
        private ICollection<Enrollment> GetAvailableEnrollments()
        {
            return new List<Enrollment>
            {
                new Enrollment
                {
                    Identification = 1,

                }
            };
        }*/

        private ICollection<Teacher> GetAvailableTeachers()
        {
            return new List<Teacher>
            {
                new Teacher
                {
                    Id = 1,
                    Name = "Teacher 1"
                },
                new Teacher
                {
                    Id = 2,
                    Name = "Teacher 2"
                },
            };
        }

        private ICollection<Course> GetAvailableCourses()
        {
            return new List<Course>
            {
                new Course
                {
                    Id = 1,
                    Title = "Course 1"
                },
                new Course
                {
                    Id = 2,
                    Title = "Course 2"
                },
                new Course
                {
                    Id = 3,
                    Title = "Course 3"
                }
            };
        }

        private Course GetCourse()
        {
            var teacherId = 2;
            return new Course
            {
                Id = 1,
                Title = "Random Course",
                TeacherId = teacherId,
                Teacher = new Teacher
                {
                    Id = teacherId,
                    Name = "Teacher 2"
                }
            };
        }

        private Teacher GetTeacher()
        {
            var id = 1;
            return new Teacher
            {
                Id = id,
                Name = "Hello",
                Courses = new List<Course>
                {
                    new Course
                    {
                        Id = 1,
                        TeacherId = id,
                        Title = "Course 1"
                    },
                    new Course
                    {
                        Id = 2,
                        TeacherId = id,
                        Title = "Course 2"
                    }
                }
            };
        }
    }
}