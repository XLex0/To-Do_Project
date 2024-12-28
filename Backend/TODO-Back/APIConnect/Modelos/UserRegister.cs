namespace APIConnect.Modelos

{
    public class UserRegister
    { 
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
    public class UserLogin
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
