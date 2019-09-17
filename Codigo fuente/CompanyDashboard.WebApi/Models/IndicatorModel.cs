using System;
using System.Collections.Generic;
using System.Linq;
using CompanyDashboard.Domain;

namespace CompanyDashboard.WebApi.Models
{
    public class IndicatorModel : Model <Indicator, IndicatorModel>
    {   
        public String Name {get; set;}
        public int Area {get; set;}
        public BinaryOperator Red {get; set;}
        public BinaryOperator Green {get; set;}
        public BinaryOperator Yellow {get; set;}

        public IndicatorModel() { }

        public IndicatorModel(Indicator entity)
        {
            SetModel(entity);
        }

        public override Indicator ToEntity() => new Indicator()
        {
            Name = this.Name,
            Area = this.Area,
            Red = this.Red,
            Green = this.Green,
            Yellow = this.Yellow,
        };

        protected override IndicatorModel SetModel(Indicator entity)
        {
            Name = entity.Name;
            Area = entity.Area;
            Red = entity.Red;
            Green = entity.Green;
            Yellow = entity.Yellow;
            return this;
        }
    }
}
