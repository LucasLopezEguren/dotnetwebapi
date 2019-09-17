using System;
using System.Collections.Generic;
using System.Linq;
using CompanyDashboard.Domain;

namespace CompanyDashboard.WebApi.Models
{
    public class UserModel : Model <User, UserModel>
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<Area> Areas { get; set; }
        public List<int> HiddenIndicators { get; set; }
        public List<int> AvailableIndicators { get; set; }
        public List<int> VisibleIndicators {get; set;}
        public string Mail { get; set; }
        public bool Admin { get; set; }

        public UserModel() { }

        public UserModel(User entity)
        {
            SetModel(entity);
        }

        public override User ToEntity() => new User()
        {
            Name = this.Name,
            Username = this.Username,
            Lastname = this.Lastname,
            Mail = this.Mail,
            Admin = this.Admin,
            Password = this.Password,
        };

        protected override UserModel SetModel(User entity)
        {
            Name = entity.Name;
            Username = entity.Username;
            Lastname = entity.Lastname;
            Mail = entity.Mail;
            Admin = entity.Admin;
            Password = entity.Password;
            return this;
        }
    }
}
