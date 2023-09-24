using Confectionery.Domain.Seedwork;

namespace Confectionery.Domain.Entities
{
    public class Client : Entity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string InstagramProfile { get; set; }
        public string MobileNumber { get; set; }

        public List<Order> Orders { get; set; }

        public Client(string fullName, string email, string instagramProfile, string mobileNumber)
        {
            FullName = fullName;
            Email = email;
            InstagramProfile = instagramProfile;
            MobileNumber = mobileNumber;
        }
    }
}
