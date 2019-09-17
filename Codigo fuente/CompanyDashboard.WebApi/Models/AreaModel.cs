using System;
using System.Collections.Generic;
using System.Linq;
using CompanyDashboard.Domain;

namespace CompanyDashboard.WebApi.Models
{
    public class AreaModel : Model <Area, AreaModel>
    {
        public string Name { get; set; }
        public List<AreaUser> AreaUsers {get; set;}
        public String DataSource {get; set;}

        public AreaModel() { }

        public AreaModel(Area entity)
        {
            SetModel(entity);
        }

        public override Area ToEntity() => new Area()
        {
            Name = this.Name,
            AreaUsers = this.AreaUsers,
            DataSource = this.DataSource,
        };

        protected override AreaModel SetModel(Area entity)
        {
            Name = entity.Name;
            AreaUsers = entity.AreaUsers;
            DataSource = entity.DataSource;
            return this;
        }
    }
}
