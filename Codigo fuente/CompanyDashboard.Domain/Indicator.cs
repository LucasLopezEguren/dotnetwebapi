using System;
using System.Collections.Generic;

namespace CompanyDashboard.Domain
{
    public class Indicator
    {
        public int ID {get; set;}
        public String Name {get; set;}
        public int Area {get; set;}
        public BinaryOperator Red {get; set;}
        public BinaryOperator Green {get; set;}
        public BinaryOperator Yellow {get; set;}

        public Indicator()
        {
        }

        public override bool Equals(object obj)
        {
            try
            {
                Indicator cast = obj as Indicator;
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
