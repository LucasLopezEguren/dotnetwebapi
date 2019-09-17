using System;
using System.Collections.Generic;

namespace CompanyDashboard.Domain
{
    public class AreaUser
    {
        public Area area { get; set; }
        public User user { get; set; }

    
        public int AreaID { get; set; }
        public int UserID { get; set; }

    }
}
