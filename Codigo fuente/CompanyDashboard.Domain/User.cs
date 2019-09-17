using System;
using System.Collections.Generic;

namespace CompanyDashboard.Domain
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<AreaUser> AreaUsers {get; set;}
        public string Mail { get; set; }
        public bool Admin { get; set; }

        public User()
        {
            this.Admin = false;
            AreaUsers = new List<AreaUser>();

        }

        public override bool Equals(object obj)
        {
            try
            {
                User cast = obj as User;
                return cast.Username.Equals(this.Username);
            }
            catch (System.NullReferenceException)
            {
                return false;
            }
        }
        
        public override int GetHashCode() => ID.GetHashCode();
    }
}
