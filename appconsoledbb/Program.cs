using appconsoledbb;
using appconsoledbb.Data;
using appconsoledbb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;

namespace consolebdd.App
{
    public class Program
    {
        public static IConfigurationRoot? Configuration { get; set; }

        public static void Main(string[] args)
        {
            try
            {
                ReadConfiguration();

                using (var db = new SchoolContext())
                {
                    Console.WriteLine("Creando base de datos...\n");

                    db.Database.EnsureDeleted();
                    db.Database.EnsureCreated();

                    Console.WriteLine("Inicializando base de datos...\n");

                    SchoolInitializer.Seed(db);

                    Console.WriteLine("Base de datos inicializada exitosamente.\n");

                    bool exit = false;
                    while (!exit)
                    {
                        Console.WriteLine("Menú Principal:");
                        Console.WriteLine("1. Tabla Students");
                        Console.WriteLine("2. Tabla Courses");
                        Console.WriteLine("3. Tabla Enrollments");
                        Console.WriteLine("4. Salir");
                        Console.Write("Seleccione una opción: ");

                        switch (Console.ReadLine())
                        {
                            case "1":
                                ShowSubMenu(db, "Students");
                                break;
                            case "2":
                                ShowSubMenu(db, "Courses");
                                break;
                            case "3":
                                ShowSubMenu(db, "Enrollments");
                                break;
                            case "4":
                                exit = true;
                                break;
                            default:
                                Console.WriteLine("Opción no válida. Intente de nuevo.");
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrió un error: {ex.Message}");
            }
        }

        private static void ReadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            Configuration = builder.Build();

            ConnectionString.DefaultConnection = Configuration["DefaultConnection"];

            Console.WriteLine("Configuración\n");
            Console.WriteLine($@"Cadena de conexión (defaultConnection) = ""{ConnectionString.DefaultConnection}""");
            Console.WriteLine();
        }

        private static void ShowSubMenu(SchoolContext db, string tableName)
        {
            bool exitSubMenu = false;
            while (!exitSubMenu)
            {
                Console.WriteLine($"\nMenú {tableName}:");
                Console.WriteLine("1. Alta");
                Console.WriteLine("2. Modificar");
                Console.WriteLine("3. Eliminar");
                Console.WriteLine("4. Volver al Menú Principal");
                Console.Write("Seleccione una opción: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        PerformAlta(db, tableName);
                        break;
                    case "2":
                        PerformModificar(db, tableName);
                        break;
                    case "3":
                        PerformEliminar(db, tableName);
                        break;
                    case "4":
                        exitSubMenu = true;
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Intente de nuevo.");
                        break;
                }
            }
        }

        private static void PerformAlta(SchoolContext db, string tableName)
        {
            try
            {
                switch (tableName)
                {
                    case "Students":
                        AltaDeStudents(db);
                        break;
                    case "Courses":
                        AltaDeCourses(db);
                        break;
                    case "Enrollments":
                        AltaDeEnrollments(db);
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrió un error al dar de alta en {tableName}: {ex.Message}");
            }
        }

        private static void PerformModificar(SchoolContext db, string tableName)
        {
            try
            {
                switch (tableName)
                {
                    case "Students":
                        ModificarStudents(db);
                        break;
                    case "Courses":
                        ModificarCourses(db);
                        break;
                    case "Enrollments":
                        ModificarEnrollments(db);
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrió un error al modificar en {tableName}: {ex.Message}");
            }
        }

        private static void PerformEliminar(SchoolContext db, string tableName)
        {
            try
            {
                switch (tableName)
                {
                    case "Students":
                        EliminarStudents(db);
                        break;
                    case "Courses":
                        EliminarCourses(db);
                        break;
                    case "Enrollments":
                        EliminarEnrollments(db);
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrió un error al eliminar en {tableName}: {ex.Message}");
            }
        }

        private static void AltaDeStudents(SchoolContext db)
        {
            Console.WriteLine("Alta de Students:");
            List<Student> students = new List<Student>();

            while (true)
            {
                try
                {
                    Console.Write("Ingrese el primer nombre: ");
                    string firstName = Console.ReadLine();
                    Console.Write("Ingrese el apellido: ");
                    string lastName = Console.ReadLine();
                    Console.Write("Ingrese la fecha de inscripción (yyyy-MM-dd): ");
                    DateTime enrollmentDate = DateTime.Parse(Console.ReadLine());

                    students.Add(new Student { FirstMidName = firstName, LastName = lastName, EnrollmentDate = enrollmentDate });

                    Console.Write("¿Desea agregar otro estudiante? (s/n): ");
                    if (Console.ReadLine().ToLower() != "s")
                        break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ocurrió un error al agregar un estudiante: {ex.Message}");
                }
            }

            try
            {
                db.Students.AddRange(students);
                db.SaveChanges();
                Console.WriteLine("Alta de Students realizada.\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrió un error al guardar los estudiantes: {ex.Message}");
            }
        }

        private static void AltaDeCourses(SchoolContext db)
        {
            Console.WriteLine("Alta de Courses:");
            List<Course> courses = new List<Course>();

            while (true)
            {
                try
                {
                    Console.Write("Ingrese el ID del curso: ");
                    int id = int.Parse(Console.ReadLine());
                    Console.Write("Ingrese el título del curso: ");
                    string title = Console.ReadLine();
                    Console.Write("Ingrese los créditos: ");
                    int credits = int.Parse(Console.ReadLine());
                    Console.Write("Ingrese el ID del departamento: ");
                    int departmentId = int.Parse(Console.ReadLine());

                    courses.Add(new Course { Id = id, Title = title, Credits = credits, DepartmentId = departmentId });

                    Console.Write("¿Desea agregar otro curso? (s/n): ");
                    if (Console.ReadLine().ToLower() != "s")
                        break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ocurrió un error al agregar un curso: {ex.Message}");
                }
            }

            try
            {
                db.Courses.AddRange(courses);
                db.SaveChanges();
                Console.WriteLine("Alta de Courses realizada.\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrió un error al guardar los cursos: {ex.Message}");
            }
        }

        private static void AltaDeEnrollments(SchoolContext db)
        {
            Console.WriteLine("Alta de Enrollments:");
            List<Enrollment> enrollments = new List<Enrollment>();

            while (true)
            {
                try
                {
                    Console.Write("Ingrese el ID del estudiante: ");
                    int studentId = int.Parse(Console.ReadLine());
                    Console.Write("Ingrese el ID del curso: ");
                    int courseId = int.Parse(Console.ReadLine());
                    Console.Write("Ingrese el grado (A, B, C, D, F): ");
                    Grade grade = (Grade)Enum.Parse(typeof(Grade), Console.ReadLine().ToUpper());

                    enrollments.Add(new Enrollment { StudentId = studentId, CourseId = courseId, Grade = grade });

                    Console.Write("¿Desea agregar otra inscripción? (s/n): ");
                    if (Console.ReadLine().ToLower() != "s")
                        break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ocurrió un error al agregar una inscripción: {ex.Message}");
                }
            }

            try
            {
                db.Enrollments.AddRange(enrollments);
                db.SaveChanges();
                Console.WriteLine("Alta de Enrollments realizada.\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrió un error al guardar las inscripciones: {ex.Message}");
            }
        }

        private static void ModificarStudents(SchoolContext db)
        {
            Console.Write("Ingrese el ID del estudiante a modificar: ");
            int id = int.Parse(Console.ReadLine());

            var student = db.Students.Find(id);
            if (student != null)
            {
                try
                {
                    Console.Write("Ingrese el nuevo primer nombre (deje en blanco para no modificar): ");
                    string firstName = Console.ReadLine();
                    if (!string.IsNullOrEmpty(firstName))
                    {
                        student.FirstMidName = firstName;
                    }

                    Console.Write("Ingrese el nuevo apellido (deje en blanco para no modificar): ");
                    string lastName = Console.ReadLine();
                    if (!string.IsNullOrEmpty(lastName))
                    {
                        student.LastName = lastName;
                    }

                    Console.Write("Ingrese la nueva fecha de inscripción (yyyy-MM-dd) (deje en blanco para no modificar): ");
                    string date = Console.ReadLine();
                    if (!string.IsNullOrEmpty(date))
                    {
                        student.EnrollmentDate = DateTime.Parse(date);
                    }

                    db.SaveChanges();
                    Console.WriteLine("Estudiante modificado exitosamente.\n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ocurrió un error al modificar el estudiante: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Estudiante no encontrado.\n");
            }
        }

        private static void ModificarCourses(SchoolContext db)
        {
            Console.Write("Ingrese el ID del curso a modificar: ");
            int id = int.Parse(Console.ReadLine());

            var course = db.Courses.Find(id);
            if (course != null)
            {
                try
                {
                    Console.Write("Ingrese el nuevo título del curso (deje en blanco para no modificar): ");
                    string title = Console.ReadLine();
                    if (!string.IsNullOrEmpty(title))
                    {
                        course.Title = title;
                    }

                    Console.Write("Ingrese los nuevos créditos (deje en blanco para no modificar): ");
                    string creditsInput = Console.ReadLine();
                    if (!string.IsNullOrEmpty(creditsInput))
                    {
                        course.Credits = int.Parse(creditsInput);
                    }

                    Console.Write("Ingrese el nuevo ID del departamento (deje en blanco para no modificar): ");
                    string departmentIdInput = Console.ReadLine();
                    if (!string.IsNullOrEmpty(departmentIdInput))
                    {
                        course.DepartmentId = int.Parse(departmentIdInput);
                    }

                    db.SaveChanges();
                    Console.WriteLine("Curso modificado exitosamente.\n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ocurrió un error al modificar el curso: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Curso no encontrado.\n");
            }
        }

        private static void ModificarEnrollments(SchoolContext db)
        {
            Console.Write("Ingrese el ID del estudiante: ");
            int studentId = int.Parse(Console.ReadLine());
            Console.Write("Ingrese el ID del curso: ");
            int courseId = int.Parse(Console.ReadLine());

            var enrollment = db.Enrollments
                .Where(e => e.StudentId == studentId && e.CourseId == courseId)
                .FirstOrDefault();

            if (enrollment != null)
            {
                try
                {
                    Console.Write("Ingrese el nuevo grado (A, B, C, D, F) (deje en blanco para no modificar): ");
                    string gradeInput = Console.ReadLine();
                    if (!string.IsNullOrEmpty(gradeInput))
                    {
                        enrollment.Grade = (Grade)Enum.Parse(typeof(Grade), gradeInput.ToUpper());
                    }

                    db.SaveChanges();
                    Console.WriteLine("Inscripción modificada exitosamente.\n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ocurrió un error al modificar la inscripción: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Inscripción no encontrada.\n");
            }
        }

        private static void EliminarStudents(SchoolContext db)
        {
            Console.Write("Ingrese el ID del estudiante a eliminar: ");
            int id = int.Parse(Console.ReadLine());

            var student = db.Students.Find(id);
            if (student != null)
            {
                try
                {
                    db.Students.Remove(student);
                    db.SaveChanges();
                    Console.WriteLine("Estudiante eliminado exitosamente.\n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ocurrió un error al eliminar el estudiante: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Estudiante no encontrado.\n");
            }
        }

        private static void EliminarCourses(SchoolContext db)
        {
            Console.Write("Ingrese el ID del curso a eliminar: ");
            int id = int.Parse(Console.ReadLine());

            var course = db.Courses.Find(id);
            if (course != null)
            {
                try
                {
                    db.Courses.Remove(course);
                    db.SaveChanges();
                    Console.WriteLine("Curso eliminado exitosamente.\n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ocurrió un error al eliminar el curso: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Curso no encontrado.\n");
            }
        }

        private static void EliminarEnrollments(SchoolContext db)
        {
            Console.Write("Ingrese el ID del estudiante: ");
            int studentId = int.Parse(Console.ReadLine());
            Console.Write("Ingrese el ID del curso: ");
            int courseId = int.Parse(Console.ReadLine());

            var enrollment = db.Enrollments
                .Where(e => e.StudentId == studentId && e.CourseId == courseId)
                .FirstOrDefault();

            if (enrollment != null)
            {
                try
                {
                    db.Enrollments.Remove(enrollment);
                    db.SaveChanges();
                    Console.WriteLine("Inscripción eliminada exitosamente.\n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ocurrió un error al eliminar la inscripción: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Inscripción no encontrada.\n");
            }
        }
    }
}
