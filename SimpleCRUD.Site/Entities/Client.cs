using System;

namespace SimpleCRUD.Site.Entities
{
    public class Client 
    {
        public virtual Guid Id { get; set; }
        public virtual string Nome { get; set; }
        public virtual string Email { get; set; }

    }
}
