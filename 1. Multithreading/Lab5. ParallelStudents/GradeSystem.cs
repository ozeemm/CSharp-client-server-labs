using System.ComponentModel;
using System.Data.SqlTypes;

namespace Lab5._ParallelStudents
{
    public class GradeSystem
    {
        public static double CalculateAverageScore(List<Grade> grades, string studentName)
        {
            var studentGrades = grades.Where(g => g.StudentName == studentName).ToList();
            if (studentGrades.Count == 0)
                return 0;

            return studentGrades.Average(g => g.Score);
        }
    }
}
