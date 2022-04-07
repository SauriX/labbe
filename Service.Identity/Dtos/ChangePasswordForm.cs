namespace Service.Identity.Dtos
{
    public class ChangePasswordForm
    {
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string token { get; set; }

        public string id { get; set; }
        
    }
}
