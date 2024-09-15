using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthorBookAuthorizedMVC.Models
{
    public class AuthorDetail
    {
        public virtual Guid Id { get; set; }
        public virtual string Street { get; set; }
        public virtual string City { get; set; }
        public virtual string State { get; set; }
        public virtual string Country { get; set; }
        public virtual bool IsActive { get; set; } = true;
        public virtual Author Author { get; set; }
    }
}