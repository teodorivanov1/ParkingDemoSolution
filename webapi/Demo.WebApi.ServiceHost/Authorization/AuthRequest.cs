namespace Demo.WebApi.ServiceHost.Authorization
{
    public class AuthRequest
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
