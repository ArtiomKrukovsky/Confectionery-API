using Confectionery.Domain.Seedwork;

namespace Confectionery.Domain.Entities
{
    public class RefreshToken : Entity
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public DateTime ExpirationTime { get; set; }

        public User User { get; set; }
    }
}
