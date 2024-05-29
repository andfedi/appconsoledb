using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace appconsoledbb.Models
{
    public enum Grade
    {
        A, B, C, D, F
    }

    public class Enrollment : IEntity
    {
        [Column("EnrollmentID")]
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        [DisplayFormat(NullDisplayText = "No grade")]
        public Grade? Grade { get; set; }

        // Propiedades de navegación
        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }
    }
}
