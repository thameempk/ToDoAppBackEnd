using Microsoft.Data.SqlClient;

namespace ToDoApp.Models
{
    public interface ITasks
    {
        public List<Tasks> GetTasks(int userid);
        public void AddTask(Tasks task);
        public void UpdateTask(int id, Tasks task);
        public void DeleteTask(int id);

    }

    public class TasksData : ITasks
    {
        private readonly IConfiguration _configuration;
        private  string ConnectionString { get; set; }

        public TasksData(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration["ConnectionString:DefaultConnection"];
        }

        public List<Tasks> GetTasks(int userid)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand($"select * from Tasks where userId = {userid}", con);
                con.Open();
                SqlDataReader red = command.ExecuteReader();
                List<Tasks> tasks = new List<Tasks>();
                while (red.Read())
                {
                    Tasks task = new Tasks();
                    task.taskId = Convert.ToInt32(red["taskId"]);
                    task.Title = red["title"].ToString();
                    task.Description = red["description"].ToString();
                    task.completed = red["completed"].ToString();
                    task.userID = Convert.ToInt32(red["userId"]);
                    task.createdDate = red["createdDate"].ToString();
                    tasks.Add(task);

                }
                return tasks;
            }
        }

        public void AddTask(Tasks task)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("insert into Tasks (title, description, userId) values (@title, @description, @userId )", con);
                command.Parameters.AddWithValue("@title", task.Title);
                command.Parameters.AddWithValue("@description", task.Description);
                command.Parameters.AddWithValue("@userId", task.userID);
                con.Open();
                command.ExecuteNonQuery();

            }
        }

        public void UpdateTask(int id, Tasks task)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand($"update Tasks set title = @title, description = @description, completed = @completed where taskId = {id} ", con);
                command.Parameters.AddWithValue("@title", task.Title);
                command.Parameters.AddWithValue("@description", task.Description);
                command.Parameters.AddWithValue("@completed", task.completed);
                con.Open();
                command.ExecuteNonQuery();

            }
        }

        public void DeleteTask(int id)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("delete from Tasks where taskId = @taskId", con);
                command.Parameters.AddWithValue("@taskId", id);
                con.Open();
                command.ExecuteNonQuery();
            }
        }



    }
}
