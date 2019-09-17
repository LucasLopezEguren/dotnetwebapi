using System;
using System.Collections.Generic;

namespace CompanyDashboard.Domain
{
    public class Session
    {
        public int ID { get; set; }
        public Guid Token { get; set; }
        public String username { get; set; }
    }
}
