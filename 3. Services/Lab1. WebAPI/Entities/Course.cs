using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace Lab1._WebAPI.Entities
{
    public class Course
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Grade>? Grades { get; set; } = new List<Grade>();
        public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

        public void CopyFrom(Course course)
        {
            this.Name = course.Name;
        }
    }
}
