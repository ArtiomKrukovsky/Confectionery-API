namespace Confectionery.API.Options
{
    public class EmailSenderOptions
    {
        public const string EmailSender = "EmailSender";

        public string Address { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
