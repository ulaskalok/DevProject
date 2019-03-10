using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.Domain
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }

        public byte[] Timestamp { get; set; }

        public Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}
