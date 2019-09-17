using CompanyDashboard;
using System;
using System.Collections.Generic;
using CompanyDashboard.Domain;
using CompanyDashboard.BusinessLogic;


namespace CompanyDashboard.BusinessLogic.Interface
{
    public interface IAreaLogic
    {
        Area AddArea(Area toAdd);
        Area GetAreaByName (string name);
        Area GetAreaByID(int iD);
        void DeleteArea(int id);
        void UpdateArea(Area updatedArea);
        void AddUser(Area area, User toAdd);
        void RemoveUser(Area area, User userToDelete);
        List<Indicator> GetIndicators(Area area);
        void AddIndicator(Area area, Indicator toAdd);
        List<Area> GetAll();
    }
    
}

