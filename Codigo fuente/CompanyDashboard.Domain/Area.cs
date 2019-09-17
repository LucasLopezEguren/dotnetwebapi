using System;
using System.Collections.Generic;

namespace CompanyDashboard.Domain
{
    public class Area
    {
        public int ID {get; set;}
        public String Name {get; set;}
        public List<AreaUser> AreaUsers {get; set;}
        public String DataSource {get; set;}
        public Area()
        {
           AreaUsers = new List<AreaUser>();
        }

        public override bool Equals(object obj)
        {
            try
            {
                Area cast = obj as Area;
                return cast.Name.Equals(this.Name);
            }
            catch (System.NullReferenceException)
            {
                return false;
            }
        }
        public override int GetHashCode() => ID.GetHashCode();
    }
}
