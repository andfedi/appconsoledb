using appconsoledbb.Models;

namespace appconsoledbb.Data
{
    public static class SchoolInitializer
    {
        public static void Seed(SchoolContext context)
        {
            if (context.Students.Any())
            {
                return;   // DB has been seeded
            }

            var instructors = new List<Instructor>
            {
                new Instructor{FirstMidName="Kim", LastName="Abercrombie", HireDate=DateTime.Parse("1995-03-11")},
                new Instructor{FirstMidName="Fadi", LastName="Fakhouri", HireDate=DateTime.Parse("2002-07-06")},
                new Instructor{FirstMidName="Roger", LastName="Harui", HireDate=DateTime.Parse("1998-07-01")},
                new Instructor{FirstMidName="Candace", LastName="Kapoor", HireDate=DateTime.Parse("2001-01-15")},
                new Instructor{FirstMidName="Roger", LastName="Zheng", HireDate=DateTime.Parse("2004-02-12")},
            };

            context.Instructors.AddRange(instructors);
            context.SaveChanges();

            var departments = new List<Department>
            {
                new Department{Name="English", Budget=350000, StartDate=DateTime.Parse("2007-09-01"), InstructorId=instructors.Single(i => i.LastName == "Abercrombie").Id},
                new Department{Name="Mathematics", Budget=100000, StartDate=DateTime.Parse("2007-09-01"), InstructorId=instructors.Single(i => i.LastName == "Fakhouri").Id},
                new Department{Name="Engineering", Budget=350000, StartDate=DateTime.Parse("2007-09-01"), InstructorId=instructors.Single(i => i.LastName == "Harui").Id},
                new Department{Name="Economics", Budget=100000, StartDate=DateTime.Parse("2007-09-01"), InstructorId=instructors.Single(i => i.LastName == "Kapoor").Id},
                new Department{Name="Chemistry", Budget=100000, StartDate=DateTime.Parse("2007-09-01"), InstructorId=instructors.Single(i => i.LastName == "Zheng").Id},
            };

            context.Departments.AddRange(departments);
            context.SaveChanges();

            var courses = new List<Course>
            {
                new Course{Id=1050, Title="Chemistry", Credits=3, DepartmentId=departments.Single(d => d.Name == "Chemistry").Id},
                new Course{Id=4022, Title="Microeconomics", Credits=3, DepartmentId=departments.Single(d => d.Name == "Economics").Id},
                new Course{Id=4041, Title="Macroeconomics", Credits=3, DepartmentId=departments.Single(d => d.Name == "Economics").Id},
                new Course{Id=1045, Title="Calculus", Credits=4, DepartmentId=departments.Single(d => d.Name == "Mathematics").Id},
                new Course{Id=3141, Title="Trigonometry", Credits=4, DepartmentId=departments.Single(d => d.Name == "Mathematics").Id},
            };

            context.Courses.AddRange(courses);
            context.SaveChanges();

            var students = new List<Student>
            {
                new Student{FirstMidName="Carson", LastName="Alexander", EnrollmentDate=DateTime.Parse("2005-09-01")},
                new Student{FirstMidName="Meredith", LastName="Alonso", EnrollmentDate=DateTime.Parse("2020-09-01")},
                new Student{FirstMidName="Arturo", LastName="Anand", EnrollmentDate=DateTime.Parse("2003-09-01")},
                new Student{FirstMidName="Gytis", LastName="Barzdukas", EnrollmentDate=DateTime.Parse("2020-09-01")},
                new Student{FirstMidName="Yan", LastName="Li", EnrollmentDate=DateTime.Parse("2002-09-01")},
            };

            context.Students.AddRange(students);
            context.SaveChanges();

            var enrollments = new List<Enrollment>
            {
                new Enrollment{StudentId=students.Single(s => s.LastName == "Alexander").Id, CourseId=courses.Single(c => c.Title == "Chemistry").Id, Grade=Grade.A},
                new Enrollment{StudentId=students.Single(s => s.LastName == "Alexander").Id, CourseId=courses.Single(c => c.Title == "Microeconomics").Id, Grade=Grade.C},
                new Enrollment{StudentId=students.Single(s => s.LastName == "Alonso").Id, CourseId=courses.Single(c => c.Title == "Macroeconomics").Id, Grade=Grade.B},
                new Enrollment{StudentId=students.Single(s => s.LastName == "Alonso").Id, CourseId=courses.Single(c => c.Title == "Calculus").Id, Grade=Grade.B},
                new Enrollment{StudentId=students.Single(s => s.LastName == "Anand").Id, CourseId=courses.Single(c => c.Title == "Trigonometry").Id, Grade=Grade.F},
            };

            context.Enrollments.AddRange(enrollments);
            context.SaveChanges();

            var officeAssignments = new List<OfficeAssignment>
            {
                new OfficeAssignment{InstructorId=instructors.Single(i => i.LastName == "Abercrombie").Id, Location="Smith 17"},
                new OfficeAssignment{InstructorId=instructors.Single(i => i.LastName == "Fakhouri").Id, Location="Gowan 27"},
                new OfficeAssignment{InstructorId=instructors.Single(i => i.LastName == "Harui").Id, Location="Thompson 304"},
                new OfficeAssignment{InstructorId=instructors.Single(i => i.LastName == "Kapoor").Id, Location="Cooper 217"},
                new OfficeAssignment{InstructorId=instructors.Single(i => i.LastName == "Zheng").Id, Location="Schwarz 340"},
            };

            context.OfficeAssignments.AddRange(officeAssignments);
            context.SaveChanges();

            var courseAssignments = new List<CourseAssignment>
            {
                new CourseAssignment{CourseId=courses.Single(c => c.Title == "Chemistry").Id, InstructorId=instructors.Single(i => i.LastName == "Abercrombie").Id},
                new CourseAssignment{CourseId=courses.Single(c => c.Title == "Microeconomics").Id, InstructorId=instructors.Single(i => i.LastName == "Fakhouri").Id},
                new CourseAssignment{CourseId=courses.Single(c => c.Title == "Macroeconomics").Id, InstructorId=instructors.Single(i => i.LastName == "Harui").Id},
                new CourseAssignment{CourseId=courses.Single(c => c.Title == "Calculus").Id, InstructorId=instructors.Single(i => i.LastName == "Kapoor").Id},
                new CourseAssignment{CourseId=courses.Single(c => c.Title == "Trigonometry").Id, InstructorId=instructors.Single(i => i.LastName == "Zheng").Id},
            };

            context.CourseAssignments.AddRange(courseAssignments);
            context.SaveChanges();
        }
    }
}
