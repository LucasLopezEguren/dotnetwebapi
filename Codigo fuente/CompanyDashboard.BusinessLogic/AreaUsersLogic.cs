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
    public class AreaUserLogic : IAreaUserLogic
    {
        private IRepository<AreaUser> repository;
        private AreaUserRepository areaUserRepository;

        public AreaUserLogic(IRepository<AreaUser> repository)
        {
            if (repository == null)
            {
                this.repository = new AreaUserRepository(ContextFactory.GetNewContext());
                this.areaUserRepository = new AreaUserRepository(ContextFactory.GetNewContext());
            }
            else
            {
                this.repository = repository;
                this.areaUserRepository = new AreaUserRepository(ContextFactory.GetNewContext());
            }
        }

        public void AddUserToArea(Area area, User userToAdd)
        {
            AreaUser toAdd = new AreaUser
            {
                AreaID = area.ID,
                area = area,
                UserID = userToAdd.ID,
                user = userToAdd
            };
            repository.Add(toAdd);
            repository.Save();
        }

        public List<User> GetUsersByArea(Area area)
        {
            List<User> userList = areaUserRepository.GetUsersByArea(area).ToList();
            return userList;
        }

        public void RemoveUserFromArea(Area area, User userToRemove)
        {
            AreaUser toDelete = GetAreaUser(area, userToRemove);
            repository.Delete(toDelete);
            repository.Save();
        }

        public bool IsUserInArea(int areaId, int userId)
        {
            List<AreaUser> areaUsers = repository.GetAll().ToList();
            foreach (AreaUser au in areaUsers)
            {
                if (au.AreaID == areaId && au.UserID == userId) return true;
            }
            return false;
        }

        public AreaUser GetAreaUser(Area area, User user)
        {
            return areaUserRepository.GetAreaUser(area, user);
        }
    }
}
