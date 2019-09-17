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
    public class StringEvaluator : IEvaluator
    {
        public virtual String[] Evaluate(Node factor)
        {
            String[] result = new String[2]{"1", factor.Text};
            return result;
        }
    }
}
