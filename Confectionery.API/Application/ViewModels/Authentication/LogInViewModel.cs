namespace Confectionery.API.Application.ViewModels.Authentication
{
    public class LogInViewModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public LogInViewModel(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}
