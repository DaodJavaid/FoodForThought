namespace FoodForThrought.Models.Admin
{
    public class AdminLogin
    {
        public string email { get; set; }
        public string Password { get; set; }

        public bool KeepLoggedIn { get; set; }
    }
}
