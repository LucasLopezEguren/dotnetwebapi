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
    public class AreaLogic : IAreaLogic
    {
        private IRepository<Area> areaRepository;
        private IRepository<Indicator> indicatorRepository;
        private IRepository<AreaUser> areaUserRepository;
        private IRepository<User> userRepository;
        private IRepository<UserIndicator> userIndicator;
        public AreaLogic(IRepository<Area> repository)
        {
            if (repository == null)
            {
                this.areaRepository = new AreaRepository(ContextFactory.GetNewContext());
                this.indicatorRepository = new IndicatorRepository(ContextFactory.GetNewContext());
                this.areaUserRepository = new AreaUserRepository(ContextFactory.GetNewContext());
                this.userRepository = new UserRepository(ContextFactory.GetNewContext());
                this.userIndicator = new UserIndicatorRepository();
            }
            else
            {
                this.indicatorRepository = new IndicatorRepository(ContextFactory.GetNewContext());
                this.areaUserRepository = new AreaUserRepository(ContextFactory.GetNewContext());
                this.userRepository = new UserRepository(ContextFactory.GetNewContext());
                this.userIndicator = new UserIndicatorRepository();                
                this.areaRepository = repository;
            }
        }

        public Area AddArea(Area area)
        {
            if (areaRepository.Has(area)) throw new AlreadyExistsException("Area ya existente");
            if (!AreaIsValid(area)) throw new NotValidException("Area no válida");
            areaRepository.Add(area);
            areaRepository.Save();
            return area;
        }

        public void UpdateArea(Area area)
        {
            if (!areaRepository.Has(area)) throw new DoesNotExistsException("Area no existe");
            if (!AreaIsValid(area)) throw new NotValidException("Area no válida");
            areaRepository.Update(area);
            areaRepository.Save();
        }

        private bool HasName(Area area)
        {
            return area.Name != null && area.Name != "";
        }
        public bool AreaIsValid(Area area)
        {
            return ((area != null) && HasName(area));
        }

        public void AddUser(Area area, User userToAdd)
        {
            
            if (!areaRepository.Has(area)) throw new DoesNotExistsException("Area no existe");
            if(!userRepository.Has(userToAdd)) throw new DoesNotExistsException("Usuario no existe");
            if (userToAdd.Admin == true) throw new UserCantBeAdminException("Solo un gerente puede pertenecer a un area");
            AreaUserLogic aul = new AreaUserLogic(areaUserRepository);
            UserLogic ul = new UserLogic(userRepository);
            List<Indicator> indicadores = indicatorRepository.GetByCondition(x=>x.Area==area.ID).ToList();
            
            foreach (Indicator ind in indicadores){
                ul.AddIndicator(userToAdd, ind);
            }     

            if(aul.IsUserInArea(area.ID, userToAdd.ID)) throw new AlreadyExistsException("Gerente ya pertenece a area");

            aul.AddUserToArea(area, userToAdd);

        }

        public void RemoveUser(Area area, User userToRemove)
        {
            if (!areaRepository.Has(area)) throw new DoesNotExistsException("Area no existe");
            if(!userRepository.Has(userToRemove)) throw new DoesNotExistsException("Usuario no existe");
            if (userToRemove.Admin == true) throw new UserCantBeAdminException("Solo un gerente puede pertenecer a un area");

            Area thisArea = areaRepository.GetByID(area.ID);   
            List<Indicator> indicators = indicatorRepository.GetByCondition(x => x.Area == thisArea.ID).ToList();
            List<AreaUser> areaUsers = areaUserRepository.GetByCondition(x => x.AreaID == thisArea.ID).ToList();
            UserLogic ul = new UserLogic(userRepository);


            foreach(AreaUser au in areaUsers){
                areaUserRepository.Delete(au);
                areaUserRepository.Update(au);
                areaUserRepository.Save();
            }

            foreach (Indicator ind in indicators){
                ul.DeleteIndicator(userToRemove, ind);
            }
            thisArea.AreaUsers.Clear();
            areaRepository.Update(thisArea);
            areaRepository.Save();                        

        }

        public void DeleteArea(int areaID)
        {
            if(areaRepository.GetByID(areaID)==null) throw new DoesNotExistsException("Area no existe");
            Area toDelete = areaRepository.GetByID(areaID);            
            List<Indicator> indicators = indicatorRepository.GetByCondition(x => x.Area == areaID).ToList();
            List<AreaUser> areaUsers = areaUserRepository.GetByCondition(x => x.AreaID == areaID).ToList();
            IndicatorLogic il = new IndicatorLogic(indicatorRepository);

            foreach(AreaUser au in areaUsers){
                areaUserRepository.Delete(au);
                areaUserRepository.Update(au);
                areaUserRepository.Save();
            }

            foreach (Indicator ind in indicators){
                il.DeleteIndicator(ind.ID);
            }
            
            toDelete.AreaUsers.Clear();
            areaRepository.Update(toDelete);
            areaRepository.Delete(toDelete);           
            areaRepository.Save();
        }        

        public void AddIndicator(Area area, Indicator indicator)
        {
            if (!areaRepository.Has(area)) throw new DoesNotExistsException("Area no existe");
            if(indicatorRepository.Has(indicator)) throw new AlreadyExistsException("Indicador ya pertenece a area"); 

            indicator.Area = area.ID;

            indicatorRepository.Update(indicator);
            indicatorRepository.Save();
        }

        public List<Indicator> GetIndicators(Area area)
        {
            if (!areaRepository.Has(area)) throw new DoesNotExistsException("Area no existe");
            return indicatorRepository.GetByCondition(x=>x.Area==area.ID).ToList();
        }

        public Area GetAreaByName(string name)
        {
            return areaRepository.GetByName(name);
        }

        public Area GetAreaByID(int id)
        {
            return areaRepository.GetByID(id);
        }

        public List<Area> GetAll()
        {
            return areaRepository.GetAll().ToList();
        }

    }
}
