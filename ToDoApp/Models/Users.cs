namespace ToDoApp.Models
{
    public class Users
    {
        public int userId { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string Role { get; set; }

    }


    public class RegisterData
    {
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }

    }


    public class Login
    {
        public string email { get; set; }
        public string password { get; set; }
    }

    public class Role
    {
        public string Admin = "admin";
        public string User = "user";
    }
}
