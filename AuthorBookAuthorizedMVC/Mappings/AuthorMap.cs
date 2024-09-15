using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AuthorBookAuthorizedMVC.Models;
using FluentNHibernate.Mapping;

namespace AuthorBookAuthorizedMVC.Mappings
{
    public class AuthorMap : ClassMap<Author>
    {
        public AuthorMap()
        {
            Table("Authors");
            Id(a => a.Id).GeneratedBy.GuidComb();
            Map(a => a.Name);
            Map(a => a.Email);
            Map(a => a.Age);
            Map(a=>a.Password);
            HasOne(a => a.AuthorDetail).Cascade.All().PropertyRef(a => a.Author).Constrained(); 
            HasMany(a => a.Books).Inverse().Cascade.All();
        }
    }
}