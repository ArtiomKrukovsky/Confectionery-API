using Confectionery.Domain.Seedwork;

namespace Confectionery.Domain.Entities
{
    public class ConfectionPicture: Entity
    {
        public string ShortName { get; set; }
        public string Extension { get; set; }
        public byte[] Content { get; set; }
    }
}
