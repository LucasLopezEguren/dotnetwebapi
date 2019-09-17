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
    public class IntEvaluator : IEvaluator
    {
        public virtual String[] Evaluate(Node factor)
        {
            Node num = factor;
            String[] result = new String[2]{"2", num.Text};
            return result;
        }
    }
}
