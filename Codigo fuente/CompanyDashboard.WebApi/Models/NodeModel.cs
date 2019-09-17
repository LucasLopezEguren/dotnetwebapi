using System;
using System.Collections.Generic;
using System.Linq;
using CompanyDashboard.Domain;

namespace CompanyDashboard.WebApi.Models
{
    public class NodeModel : Model <Node, NodeModel>
    {   
        public int Type { get; set; }
        public int Area { get; set; }
        public String Text { get; set; }
        public String Sign {get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }

        public NodeModel() { }

        public NodeModel(Node entity)
        {
            SetModel(entity);
        }

        public override Node ToEntity() => new BinaryOperator()
        {
            Type = this.Type,
            Area = this.Area,
            Text = this.Text,
            Sign = this.Sign,
            Left = this.Left,
            Right = this.Right
        };

        protected override NodeModel SetModel(Node entity)
        {
            Type = entity.Type;
            Area = entity.Area;
            Text = entity.Text;
            Sign = entity.Sign;
            Left = entity.Left;
            Right = entity.Right;
            return this;
        }
    }
}
