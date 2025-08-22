namespace Infrastructure.DataModels
{
    public class HomeWork
    {
        public int Id { get; set; }
        public int HomeTaskId { get; set; }
        public int StudentId { get; set; }
        public int Score { get; set; } = 0;
        public DateTime TaskCompletionDate { get; set; }
        public string Material { get; set; }
        public string StudentComment { get; set; }
        public string TeacherComment { get; set; }
    }
}
