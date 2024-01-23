using Microsoft.Data.SqlClient;

namespace ToDoApp.Models
{
    public interface IUsers
    {
        public void Register(Users users);
        public List<Users> GetUsers();
        public Users Login(Login login);
        
    }

    public class UserData : IUsers
    {
        private readonly IConfiguration _configuration;
        private  string ConnectionString { get; set; }

        public UserData(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration["ConnectionString:DefaultConnection"];
        }

        public List<Users> GetUsers()
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("select * from Register", con);
                con.Open();
                SqlDataReader red = command.ExecuteReader();
                List<Users> users = new List<Users>();
                while (red.Read())
                {
                    Users user = new Users();
                    user.userId = Convert.ToInt32(red["userId"]);
                    user.name = red["userName"].ToString();
                    user.email = red["email"].ToString();
                    user.password = red["password"].ToString();
                    user.Role = red["Role"].ToString();
                    users.Add(user);
                }
                return users;
            }
        }

        public void Register(Users users)
        {
            
            using( SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("insert into Register(userName, email, password, Role) values(@userName, @email, @password, @role)", con);
                command.Parameters.AddWithValue("@userName", users.name);
                command.Parameters.AddWithValue("@email", users.email);
                command.Parameters.AddWithValue("@password", users.password);
                command.Parameters.AddWithValue("@role", users.Role);
                con.Open();
                command.ExecuteNonQuery();
            }
        }

        public Users Login(Login login)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("select * from Register where email = @email and password = @password", con);
                command.Parameters.AddWithValue("@email", login.email);
                command.Parameters.AddWithValue("@password", login.password);
                
                con.Open();
                command.ExecuteNonQuery();
                SqlDataReader red = command.ExecuteReader();
                Users user = new Users();
                while (red.Read())
                {
                    
                    user.userId = Convert.ToInt32(red["userId"]);
                    user.name = red["userName"].ToString();
                    user.email = red["email"].ToString();
                    user.password = red["password"].ToString();
                    user.Role = red["Role"].ToString();
                }
                return user;
            }
            
        }


    }
}
