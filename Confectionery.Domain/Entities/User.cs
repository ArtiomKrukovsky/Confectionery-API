using Confectionery.Domain.Enums;
using Confectionery.Domain.Seedwork;

namespace Confectionery.Domain.Entities
{
    public class User : Entity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public Role Role { get; set; }
        public RefreshToken RefreshToken { get; set; }
    }
}
