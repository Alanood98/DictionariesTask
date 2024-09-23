using System;
using System.Collections;

namespace Dictionaries
{
    internal class Program
    {
        static void Main(string[] args)
        {

            while (true)
            {
                Console.WriteLine("Course Enrollment System ");
                Console.WriteLine("1. Add a new course");
                Console.WriteLine("2. Remove Course");
                Console.WriteLine("3. Enroll a student in a course");
                Console.WriteLine("4. Remove a student from a course");
                Console.WriteLine("5. Display all students in a course");
                Console.WriteLine("6. Display all courses and their students");
                Console.WriteLine("7. Find courses with common students");
                Console.WriteLine("8. Withdraw a Student from All Courses");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AddNewCourse();
                        break;
                    case "2":
                        RemoveCourse();
                        break;
                    case "3":
                        EnrollStudentInCourse();
                        break;
                    case "4":
                        RemoveStudentFromCourse();
                        break;
                    case "5":
                        DisplayAllStudentsInCourse();
                        break;
                    case "6":
                        DisplayAllCoursesAndTheirStudents();
                        break;
                    case "7":
                        FindCoursesWithCommonStudents();
                        break;
                    case "8":
                        WithdrawStudentFromAllCourses();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }

        }






    }
}
}
