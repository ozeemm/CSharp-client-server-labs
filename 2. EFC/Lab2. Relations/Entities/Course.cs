using System.ComponentModel.DataAnnotations;

namespace Lab2._Relations.Entities
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; }

        public virtual ICollection<Grade>? Grades { get; set; } = new List<Grade>();
        public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
