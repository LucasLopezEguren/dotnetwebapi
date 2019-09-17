using CompanyDashboard;
using System;
using System.Collections.Generic;
using CompanyDashboard.Domain;
using CompanyDashboard.BusinessLogic;


namespace CompanyDashboard.BusinessLogic.Interface
{
    public interface IIndicatorLogic
    {

         Indicator AddIndicator(Indicator toAdd);
         void DeleteIndicator(int id);
         void UpdateIndicator(Indicator updatedIndicator);
         IEnumerable<Indicator> GetAllIndicators();
         Indicator GetById (int id);
    }
    
}

