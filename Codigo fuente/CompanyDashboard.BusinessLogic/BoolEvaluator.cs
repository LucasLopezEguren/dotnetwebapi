using System;
using System.Linq;
using System.Collections.Generic;
using CompanyDashboard.BusinessLogic.Interface;
using CompanyDashboard.Domain;
using CompanyDashboard.DataAccess.Interface;
using CompanyDashboard.DataAccess;
using CompanyDashboard.BusinessLogic.Exceptions;

namespace CompanyDashboard.BusinessLogic
{
    public class BoolEvaluator : IEvaluator
    {
        public virtual String[] Evaluate(Node factor)
        {
            NodeLogic logic = new NodeLogic(null);
            String[] result = new String[2];
            result[0] = "4";
            if(factor.Sign != null){                
                result[1] = logic.Compare(factor.Left, factor.Right, factor.Sign).ToString();
                return result;
            }
            result[1] = factor.Text;
            return result;
        }
    }
}
