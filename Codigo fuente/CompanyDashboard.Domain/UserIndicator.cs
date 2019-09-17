using System;
using System.Collections.Generic;

namespace CompanyDashboard.Domain
{
    public class UserIndicator
    {
        public int ID { get; set; }
        public int user { get; set; }
        public int indicator { get; set; }
        public String name { get; set; }
        public int order { get; set; }
        public bool visible { get; set; }

    }
}
