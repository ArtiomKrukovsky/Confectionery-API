namespace Confectionery.API.Application.ViewModels.Authentication
{
    public class RefreshTokenViewModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public RefreshTokenViewModel(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}
