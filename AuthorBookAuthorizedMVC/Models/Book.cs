﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthorBookAuthorizedMVC.Models
{
    public class Book
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Genre { get; set; }
        public virtual string Description { get; set; }
        public virtual Author Author { get; set; }
    }
}