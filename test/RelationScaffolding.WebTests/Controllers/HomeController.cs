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
                Course = GetCourse(),
                Student = GetStudent(),
                Enrollment = GetEnrollment(),
                AvailableCourses = GetAvailableCourses(),
                AvailableTeachers = GetAvailableTeachers(),
                AvailableStudents = GetAvailableStudents(),
                AvailableActivities = GetAvailableActivities()
            };
        }

        private Enrollment GetEnrollment()
        {
            var courseId = 1;
            var studentId = 2;
            return new Enrollment
            {
                Identification = 1,
                CourseId = courseId,
                Course = new Course
                {
                    Id = courseId,
                    Title = "Course 1"
                },
                StudentId = studentId,
                Student = new Student
                {
                    Id = studentId,
                    Name = "Student 2"
                }
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
                }.ToArray()
            };
        }

        private ICollection<Student> GetAvailableStudents()
        {
            return new List<Student>
            {
                new Student
                {
                    Id = 1,
                    Name = "Student 1"
                },
                new Student
                {
                    Id = 2,
                    Name = "Student 2"
                },
            };
        }

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

        private ICollection<Activity> GetAvailableActivities()
        {
            return new List<Activity>
            {
                new Activity
                {
                    Id = 1,
                    Name = "Activity 1"
                },
                new Activity
                {
                    Id = 2,
                    Name = "Activity 2"
                },
                new Activity
                {
                    Id = 3,
                    Name = "Activity 3"
                },
                new Activity
                {
                    Id = 4,
                    Name = "Activity 4"
                },
                new Activity
                {
                    Id = 5,
                    Name = "Activity 5"
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
                Name = "Teacher 1",
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
                },
                ActivityTeachers = new List<ActivityTeachers>
                {
                    new ActivityTeachers
                    {
                        Id = 1,
                        RegistrationCode = "Reg#1",
                        TeacherId = id,
                        ActivityId = GetAvailableActivities().Skip(1).First().Id
                    },
                    new ActivityTeachers
                    {
                        Id = 2,
                        RegistrationCode = "Reg#2",
                        TeacherId = id,
                        ActivityId = GetAvailableActivities().Skip(3).First().Id
                    }
                }
            };
        }
    }
}