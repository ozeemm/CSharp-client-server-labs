namespace Lab1._SimpleApp
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }
        public string? Description { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}\n" +
                $"Title: {Title}\n" +
                $"Duration: {Duration}" +
                (Description != null ? $"\nDescription: {Description}" : "");
        }
    }
}
