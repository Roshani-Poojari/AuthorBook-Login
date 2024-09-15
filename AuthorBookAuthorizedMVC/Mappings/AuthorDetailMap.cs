using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AuthorBookAuthorizedMVC.Models;
using FluentNHibernate.Mapping;

namespace AuthorBookAuthorizedMVC.Mappings
{
    public class AuthorDetailMap : ClassMap<AuthorDetail>
    {
        public AuthorDetailMap()
        {
            Table("AuthorDetails");
            Id(ad => ad.Id).GeneratedBy.GuidComb();
            Map(ad => ad.Street);
            Map(ad => ad.City);
            Map(ad => ad.State);
            Map(ad => ad.Country);
            Map(ad=>ad.IsActive);
            References(ad => ad.Author).Column("AuthorId").Unique().Cascade.None();//unique remove
        }
    }
}