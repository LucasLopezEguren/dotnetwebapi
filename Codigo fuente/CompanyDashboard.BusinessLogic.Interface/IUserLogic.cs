using CompanyDashboard;
using System;
using System.Collections.Generic;
using CompanyDashboard.Domain;
using CompanyDashboard.BusinessLogic;


namespace CompanyDashboard.BusinessLogic.Interface
{
    public interface IUserLogic
    {
        User AddUser(User toAdd);
        void DeleteUser(int id);
        void UpdateUser(User updatedUser);
        IEnumerable<User> GetAllUsers();
        User GetUserByName(string username);
        User GetUserByID(int iD);
        bool IsAdmin(User user);
        void AddIndicator(User user, Indicator newIndicator);
        void HideIndicator(User user, Indicator visibleIndicator);
        void ShowIndicator(User user, Indicator hiddenIndicator);
        void DeleteIndicator(User user, Indicator indicator);
        List<Indicator> GetAllIndicators(User user);
        List<Indicator> GetIndicatorsByUser(User user);
        List<Indicator> GetVisibleIndicators(User user);
        List<Indicator> ReorderUserIndicators(User user, List<Indicator> indicators);
        List<String> GetLoginUsersList();
        List<UserIndicator> GetAllHiddenIndicatros();
        void RenameIndicator(int user, int indicator, String newName);
        List<Log> GetActionsBetweenDatesList(DateTime lowerDate, DateTime higherDate);
    }

}

