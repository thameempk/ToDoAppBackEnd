namespace ToDoApp.Models
{
    public class Tasks
    {
        public int taskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string completed {  get; set; }
        public int userID { get; set; }
    }
}
