using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Lab1._WebAPI.Entities
{
    public class Grade
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        [Required]
        public decimal Score { get; set; }

        public void CopyFrom(Grade grade)
        {
            this.StudentId = grade.StudentId;
            this.CourseId = grade.CourseId;
            this.Score = grade.Score;
        }
    }
}
