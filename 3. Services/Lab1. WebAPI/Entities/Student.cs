using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab1._WebAPI.Entities
{
    public class Student
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Fullname { get; set; }
        public int Age { get; set; }

        public List<Grade> Grades { get; set; } = new();

        public void CopyFrom(Student student)
        {
            this.Fullname = student.Fullname;
            this.Age = student.Age;
        }
    }
}
