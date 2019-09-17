using CompanyDashboard;
using System;
using System.Collections.Generic;
using CompanyDashboard.Domain;
using CompanyDashboard.BusinessLogic;


namespace CompanyDashboard.BusinessLogic.Interface
{
   public interface ISessionLogic: IDisposable
    {
        Guid Login(string username, string password);
        User GetUserFromToken(Guid token);
    }
}

