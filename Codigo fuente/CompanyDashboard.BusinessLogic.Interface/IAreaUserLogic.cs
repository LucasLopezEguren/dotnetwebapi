using CompanyDashboard;
using System;
using System.Collections.Generic;
using CompanyDashboard.Domain;
using CompanyDashboard.BusinessLogic;


namespace CompanyDashboard.BusinessLogic.Interface
{
    public interface IAreaUserLogic
    {
        void AddUserToArea(Area area, User user);
        void RemoveUserFromArea(Area area, User user);
        AreaUser GetAreaUser(Area area, User user);
        List<User> GetUsersByArea(Area area);
    }
    
}

