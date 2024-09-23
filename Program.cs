using System;
using System.Collections;

namespace Dictionaries
{
    internal class Program
    {
        // Dictionary to store courses and enrolled students
        static Dictionary<string, HashSet<string>> courses = new Dictionary<string, HashSet<string>>();
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

//************************************************************************************************************************
         static void AddNewCourse()
        {
            Console.WriteLine("Enter course code to add (or press any key to stop):");
            string coursCode = Console.ReadLine();
            if (!courses.ContainsKey(coursCode))
            {
                courses[coursCode] = new HashSet<string>();
                Console.WriteLine($"Course {coursCode} added.");
            }
            else
            {
                Console.WriteLine($"Course {coursCode} already exists.");
            }


        }

 //**************************************************************************************************************************

        // Function to remove a course
        static void RemoveCourse(string courseCode)
        {
            Console.WriteLine("Enter course code you want to remove it (or press any key to stop):");
            string removeCourseName = Console.ReadLine();

            if (courses.ContainsKey(courseCode))
            {
                // Check if the course has no enrolled students
                if (courses[courseCode].Count == 0)
                {
                    courses.Remove(courseCode);
                    Console.WriteLine($"Course {courseCode} removed.");
                }
                else
                {
                    // Inform the user that the course cannot be removed
                    Console.WriteLine($"Cannot remove {courseCode} because it has enrolled students.");
                }
            }
            else
            {
                // Inform the user that the course does not exist
                Console.WriteLine($"Course {courseCode} does not exist.");
            }
        }

        //********************************************************************************************************

        // Function to enroll a student in a course
        static void EnrollStudent(string courseCode, string studentName)
        {
            if (courses.ContainsKey(courseCode))
            {
                if (courses[courseCode].Add(studentName))
                {
                    Console.WriteLine($"{studentName} enrolled in {courseCode}.");
                }
                else
                {
                    Console.WriteLine($"{studentName} is already enrolled in {courseCode}.");
                }
            }
            else
            {
                Console.WriteLine($"Course {courseCode} does not exist.");
            }
        }

        //*****************************************************************************************************************

        // Function to remove a student from a course
        static void RemoveStudentFromCourse(string courseCode, string studentName)
        {
            if (courses.ContainsKey(courseCode))
            {
                if (courses[courseCode].Remove(studentName))
                {
                    Console.WriteLine($"{studentName} unenrolled from {courseCode}.");
                }
                else
                {
                    Console.WriteLine($"{studentName} is not enrolled in {courseCode}.");
                }
            }
            else
            {
                Console.WriteLine($"Course {courseCode} does not exist.");
            }
        }



    }
}

