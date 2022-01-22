using System;

namespace Domain
{
    public abstract class Entity
    {
        public DateTime? CreatedAt { get; protected set; }
        public DateTime? ModifiedAt { get; protected set; }
    }
}
