using System;
using System.Collections.Generic;

namespace Dictionaries
{
    internal class Program
    {
        static Dictionary<string, HashSet<string>> courses = new Dictionary<string, HashSet<string>>();
        static List<(string studentName, string courseCode)> WaitList = new List<(string, string)>();
        static Dictionary<string, int> courseCapacity = new Dictionary<string, int>();

        static void Main(string[] args)
        {
            InitializeStartupData();



            while (true)
            {
                Console.WriteLine("\nCourse Enrollment System ");
                Console.WriteLine("1. Add a new course");
                Console.WriteLine("2. Remove Course");
                Console.WriteLine("3. Enroll a student in a course");
                Console.WriteLine("4. Remove a student from a course");
                Console.WriteLine("5. Display all students in a course");
                Console.WriteLine("6. Display all courses and their students");
                Console.WriteLine("7. Find courses with common students");
                Console.WriteLine("8. Withdraw a Student from All Courses");
                Console.WriteLine("0. Exit");

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
                    case "0":
                        Console.WriteLine("Exiting the program.");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
//*********************************************************************************************************************
        static void AddNewCourse()
        {
            Console.WriteLine("Enter course code to add:");
            string courseCode = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(courseCode))
            {
                Console.WriteLine("Course code cannot be empty.");
                return;
            }
            if (!courses.ContainsKey(courseCode))
            {
                courses[courseCode] = new HashSet<string>();//used to store additional details (e.g., student names )

                Console.WriteLine("Enter the course capacity:");
                if (int.TryParse(Console.ReadLine(), out int capacity) && capacity > 0)
                {
                    //Store the Capacity 
                    courseCapacity[courseCode] = capacity;
                    Console.WriteLine($"Course {courseCode} added with a capacity of {capacity}.");
                }
                else
                {
                    
                    Console.WriteLine("Invalid capacity. Course not added.");
                }
            }
            else
            {
                //Handle Duplicate Course Code:
                Console.WriteLine($"Course {courseCode} already exists.");
            }
        }
//***************************************************************************************************************
        static void RemoveCourse()
        {
            Console.WriteLine("Enter course code you want to remove:");
            string courseCode = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(courseCode))
            {
                Console.WriteLine("Course code cannot be empty.");
                return;
            }
            //Check if the Course Exists
            if (courses.ContainsKey(courseCode))
            {

                //Check if the Course has Enrolled Students
                if (courses[courseCode].Count == 0)
                {
                    courses.Remove(courseCode);
                    courseCapacity.Remove(courseCode);  
                    Console.WriteLine($"Course {courseCode} removed.");
                }
                else
                {
                    Console.WriteLine($"Cannot remove {courseCode} because it has enrolled students.");
                }
            }
            else
            {
                //Handle Case Where the Course Does Not Exist
                Console.WriteLine($"Course {courseCode} does not exist.");
            }
        }
        //******************************************************************************************************************
        static void EnrollStudentInCourse()
        {
            // Display available courses and their capacities
            DisplayCoursesAndCapacities();

            Console.WriteLine("Enter course code:");
            string courseCode = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(courseCode))
            {
                Console.WriteLine("Course code cannot be empty.");
                return;
            }

            Console.WriteLine("Enter student name:");
            string studentName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(studentName))
            {
                Console.WriteLine("Student name cannot be empty.");
                return;
            }

            if (courses.ContainsKey(courseCode))
            {
                if (courses[courseCode].Contains(studentName))
                {
                    Console.WriteLine($"{studentName} is already enrolled in {courseCode}.");
                    return;
                }

                if (courses[courseCode].Count < courseCapacity[courseCode])
                {
                    courses[courseCode].Add(studentName);
                    Console.WriteLine($"{studentName} enrolled in {courseCode}.");
                }
                else
                {
                    Console.WriteLine($"Course {courseCode} is full. {studentName} has been added to the waitlist.");
                    WaitList.Add((studentName, courseCode));
                }
            }
            else
            {
                Console.WriteLine($"Course {courseCode} does not exist.");
            }
        }


        //*****************************************************************************************************************
        static void RemoveStudentFromCourse()
        {
            Console.WriteLine("Enter course code:");
            string courseCode = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(courseCode))
            {
                Console.WriteLine("Course code cannot be empty.");
                return;
            }

            Console.WriteLine("Enter student name:");
            string studentName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(studentName))
            {
                Console.WriteLine("Student name cannot be empty.");
                return;
            }

            if (courses.ContainsKey(courseCode))
            {
                if (courses[courseCode].Remove(studentName))
                {
                    Console.WriteLine($"{studentName} unenrolled from {courseCode}.");

                    // Check if there are students on the waitlist for this course
                    for (int i = 0; i < WaitList.Count; i++)
                    {
                        if (WaitList[i].courseCode == courseCode)
                        {
                            // Check if the course has room now
                            if (courses[courseCode].Count < courseCapacity[courseCode])
                            {
                                courses[courseCode].Add(WaitList[i].studentName);
                                Console.WriteLine($"{WaitList[i].studentName} has been enrolled in {courseCode} from the waitlist.");
                                WaitList.RemoveAt(i);
                            }
                            break; // Only enroll one student from the waitlist
                        }
                    }
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

//**************************************************************************************************************************
        static void DisplayAllStudentsInCourse()
        {
            Console.WriteLine("Enter course code:");
            string courseCode = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(courseCode))
            {
                Console.WriteLine("Course code cannot be empty.");
                return;
            }

            if (courses.ContainsKey(courseCode))
            {
                Console.WriteLine($"Students enrolled in {courseCode}:");
                foreach (var student in courses[courseCode])
                {
                    Console.WriteLine(student);
                }
            }
            else
            {
                Console.WriteLine($"Course {courseCode} does not exist.");
            }
        }

        //************************************************************************************

        static void DisplayAllCoursesAndTheirStudents()
        {
            Console.WriteLine("All Courses and Enrolled Students:");
            foreach (var course in courses.Keys)
            {
                Console.WriteLine($"Students enrolled in {course}:");
                foreach (var student in courses[course])
                {
                    Console.WriteLine(student);
                }
            }
        }

        //**********************************************************************************************
        static void FindCoursesWithCommonStudents()
        {
            Console.WriteLine("Enter the first course code:");
            string courseCode1 = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(courseCode1))
            {
                Console.WriteLine("Course code cannot be empty.");
                return;
            }

            Console.WriteLine("Enter the second course code:");
            string courseCode2 = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(courseCode2))
            {
                Console.WriteLine("Course code cannot be empty.");
                return;
            }

            if (courses.ContainsKey(courseCode1) && courses.ContainsKey(courseCode2))
            {
                HashSet<string> commonStudents = new HashSet<string>(courses[courseCode1]);
                //commonStudents.IntersectWith(courses[courseCode2]);

                if (commonStudents.Count > 0)
                {
                    Console.WriteLine($"Common students in {courseCode1} and {courseCode2}:");
                    foreach (var student in commonStudents)
                    {
                        Console.WriteLine(student);
                    }
                }
                else
                {
                    Console.WriteLine($"No common students found in {courseCode1} and {courseCode2}.");
                }
            }
            else
            {
                Console.WriteLine($"One or both courses do not exist.");
            }
        }

//***************************************************************************************************************
        static void WithdrawStudentFromAllCourses()
        {
            Console.WriteLine("Enter student name to withdraw:");
            string studentName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(studentName))
            {
                Console.WriteLine("Student name cannot be empty.");
                return;
            }

            foreach (var course in courses.Keys)
            {
                courses[course].Remove(studentName);
            }
            Console.WriteLine($"{studentName} has been withdrawn from all courses.");
        }

        //***************************************************************************************************************

        //DisplayCoursesAndCapacities that will show the course codes along with their capacities.
        static void DisplayCoursesAndCapacities()
        {
            Console.WriteLine("Available Courses and Their Capacities:");
            foreach (var course in courseCapacity)
            {
                Console.WriteLine($"Course: {course.Key}, Capacity: {course.Value}");
            }
        }

        static void InitializeStartupData()
        {
            // Example data: Courses and their enrolled students (cross-over students)
            courses["CS101"] = new HashSet<string> { "Alice", "Bob", "Charlie" };   // CS101 has Alice, Bob, Charlie
            courses["MATH202"] = new HashSet<string> { "David", "Eva", "Bob" };     // MATH202 has David, Eva, and Bob (cross-over with CS101)
            courses["ENG303"] = new HashSet<string> { "Frank", "Grace", "Charlie" };// ENG303 has Frank, Grace, and Charlie (cross-over with CS101)
            courses["BIO404"] = new HashSet<string> { "Ivy", "Jack", "David" };     // BIO404 has Ivy, Jack, and David (cross-over with MATH202)
                                                                                    // Set course capacities (varying)
            courseCapacity["CS101"] = 3;  // CS101 capacity of 3 (currently full)
            courseCapacity["MATH202"] = 5; // MATH202 capacity of 5 (can accept more students)
            courseCapacity["ENG303"] = 3;  // ENG303 capacity of 3 (currently full)
            courseCapacity["BIO404"] = 4;  // BIO404 capacity of 4 (can accept more students)
                                           // Waitlist for courses (students waiting to enroll in full courses)
            WaitList.Add(("Helen", "CS101"));   // Helen waiting for CS101
            WaitList.Add(("Jack", "ENG303"));   // Jack waiting for ENG303
            WaitList.Add(("Alice", "BIO404"));  // Alice waiting for BIO404
            WaitList.Add(("Eva", "ENG303"));    // Eva waiting for ENG303
            Console.WriteLine("Startup data initialized.");
        }

    }
}
