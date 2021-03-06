﻿using FluentNHibernate.Mapping;
using IngolStadtNatur.Entities.NH.Common;
using IngolStadtNatur.Entities.NH.Observations;

namespace IngolStadtNatur.Entities.NH.Media
{
    public class Shot : BaseEntity
    {
        public virtual bool IsPublic { get; set; }
        public virtual string Name { get; set; }
        public virtual Observation Observation { get; set; }
    }

    public class ShotMap : ClassMap<Shot>
    {
        public ShotMap()
        {
            Table("Shots");

            Id(m => m.Id);
            Version(m => m.Version);

            Map(m => m.IsPublic);
            Map(m => m.Name);
            References(m => m.Observation)
                .Column("ObservationRef")
                .Cascade.All();
        }
    }
}