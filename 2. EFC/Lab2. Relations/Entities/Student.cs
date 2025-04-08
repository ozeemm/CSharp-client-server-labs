using System.ComponentModel.DataAnnotations;

namespace Lab2._Relations.Entities
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Fullname { get; set; }
        public int Age { get; set; }
        public string? Address { get; set; }

        public virtual ICollection<Grade>? Grades { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
