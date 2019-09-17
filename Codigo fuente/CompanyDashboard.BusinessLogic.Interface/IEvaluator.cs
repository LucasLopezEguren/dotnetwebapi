using CompanyDashboard;
using System;
using System.Collections.Generic;
using CompanyDashboard.Domain;
using CompanyDashboard.BusinessLogic;


namespace CompanyDashboard.BusinessLogic.Interface
{
    public interface IEvaluator
    {
        String[] Evaluate(Node factor);
    }
    
}

