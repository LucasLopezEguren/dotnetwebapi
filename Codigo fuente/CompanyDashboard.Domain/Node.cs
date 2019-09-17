using System;
using System.Collections.Generic;

namespace CompanyDashboard.Domain
{
    public class Node
    {
        public int ID { get; set; }
        public int Type { get; set; }
        public int Area { get; set; }
        public String Text { get; set; }
        public String Sign {get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }

        public String GetText() {
            if(this.Sign != null){
                String toReturn = "("+this.Left.GetText() + " " + this.Sign + " " + this.Right.GetText() + ")";
                return toReturn;
            }
            return this.Text;
        }
    }
}
