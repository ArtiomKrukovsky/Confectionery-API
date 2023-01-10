﻿using Confectionery.Domain.Seedwork;

namespace Confectionery.Domain.Entities
{
    public class ConfectionPicture: Entity
    {
        public Guid ConfectionId { get; set; }
        public string ShortName { get; set; }
        public string Extension { get; set; }
        public byte[] Content { get; set; }

        public Confection Confection { get; set; }
    }
}
