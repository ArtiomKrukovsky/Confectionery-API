namespace Confectionery.API.Application.ViewModels
{
    public class ConfectionPictureViewModel : BaseViewModel
    {
        public string ShortName { get; set; }
        public string Extension { get; set; }
        public byte[] Content { get; set; }
    }
}
