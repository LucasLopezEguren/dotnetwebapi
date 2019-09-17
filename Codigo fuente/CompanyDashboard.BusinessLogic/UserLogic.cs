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
    public class UserLogic : IUserLogic
    {
        private IRepository<User> userRepository;
        private UserIndicatorRepository userIndicatorRepository;
        private IndicatorRepository iRepository;
        private LogRepository logRepository;
        public UserLogic(IRepository<User> repository)
        {
            if (repository == null)
            {
                this.userRepository = new UserRepository(ContextFactory.GetNewContext());
                this.iRepository = new IndicatorRepository(ContextFactory.GetNewContext());
                this.logRepository = new LogRepository(ContextFactory.GetNewContext());
                this.userIndicatorRepository = new UserIndicatorRepository();
            }
            else
            {
                this.userRepository = repository;
                this.userIndicatorRepository = new UserIndicatorRepository();
                this.iRepository = new IndicatorRepository(ContextFactory.GetNewContext());
                this.logRepository = new LogRepository(ContextFactory.GetNewContext());
            }
        }

        public User AddUser(User user)
        {
            if (userRepository.Has(user)) throw new AlreadyExistsException("Usuario ya existente");
            if (userRepository.GetByCondition(x => x.Username == user.Username).Any()) throw new AlreadyExistsException("Username ya está en uso");
            if (!UserIsValid(user)) throw new NotValidException("Usuario no válido");
            userRepository.Add(user);
            userRepository.Save();
            return user;
        }

        public void DeleteUser(int id)
        {
            User user = GetUserByID(id);
            if (!userRepository.Has(user)) throw new DoesNotExistsException("Usuario no existe");
            userRepository.Delete(user);
            userRepository.Save();
        }

        public void UpdateUser(User user)
        {
            if (!userRepository.Has(user)) throw new DoesNotExistsException("Usuario no existe");
            if (userRepository.GetByCondition(x => x.Username == user.Username).Any()) throw new AlreadyExistsException("Username ya está en uso");
            if (!UserIsValid(user)) throw new NotValidException("Usuario no válido");
            userRepository.Update(user);
            userRepository.Save();
        }

        public bool UserIsValid(User user)
        {
            return ((user != null) && HasName(user) && HasLastname(user) && HasValidEmail(user) && HasValidPassword(user) && HasValidUsername(user));
        }
        private bool HasName(User user)
        {
            return user.Name != null && user.Name != "";
        }

        public bool HasValidEmail(User user)
        {
            try
            {
                var direccionEmail = new System.Net.Mail.MailAddress(user.Mail);
                return direccionEmail.Address == user.Mail;
            }
            catch
            {
                return false;
            }
        }

        private bool HasLastname(User user)
        {
            return user.Lastname != null && user.Lastname != "";
        }

        private bool HasValidPassword(User user)
        {
            return user.Password != null && (user.Password.Length >= 3);
        }

        private bool HasValidUsername(User user)
        {
            return user.Username != null && (user.Username.Length >= 3);
        }

        public IEnumerable<User> GetAllUsers()
        {
            IEnumerable<User> toReturn = userRepository.GetAll();
            return toReturn;
        }

        public User GetUserByName(string username)
        {
            return userRepository.GetByName(username);
        }

        public User GetUserByID(int id)
        {
            return userRepository.GetByID(id);
        }


        public bool IsAdmin(User user)
        {
            if (!userRepository.Has(user)) throw new DoesNotExistsException("Usuario no existe");
            return user.Admin == true;
        }

        public void AddIndicator(User user, Indicator indicator)
        {
            //   if ((GetIndicatorsByUser(user).Contains(indicator))) throw new AlreadyExistsException("Usuario ya tiene este indicador");
            if (IsAdmin(user)) throw new UserCantBeAdminException("Usuario no puede tener rol de administrador");
            UserIndicator toAdd = new UserIndicator
            {
                user = user.ID,
                indicator = indicator.ID,
            };
            userIndicatorRepository.Add(toAdd);
            userIndicatorRepository.Save();
        }

        public void RenameIndicator(int user, int indicator, String newName)
        {
            List<UserIndicator> toChange = userIndicatorRepository.GetByCondition(x => x.user == user && x.indicator == indicator).ToList();
            toChange[0].name = newName;
            userIndicatorRepository.Update(toChange[0]);
            userIndicatorRepository.Save();
        }


        public void HideIndicator(User user, Indicator indicator)
        {
            UserIndicator toHide = userIndicatorRepository.Get(user, indicator);
            if (!userIndicatorRepository.Has(toHide)) throw new DoesNotExistsException("Indicador no existe");
            toHide.visible = false;
            userIndicatorRepository.Update(toHide);
            userIndicatorRepository.Save();
        }

        public void ShowIndicator(User user, Indicator indicator)
        {
            UserIndicator toShow = userIndicatorRepository.Get(user, indicator);
            if (!userIndicatorRepository.Has(toShow)) throw new DoesNotExistsException("Indicador no existe");
            toShow.visible = true;
            userIndicatorRepository.Update(toShow);
            userIndicatorRepository.Save();
        }

        public void DeleteIndicator(User user, Indicator indicator)
        {
            UserIndicator toDelete = userIndicatorRepository.Get(user, indicator);
            if (!userIndicatorRepository.Has(toDelete)) throw new DoesNotExistsException("Indicador no existe");
            userIndicatorRepository.Delete(toDelete);
            userIndicatorRepository.Save();
        }

        public List<Indicator> GetIndicatorsByUser(User user)
        {
            List<UserIndicator> userIndicator = userIndicatorRepository.GetIndicatorsByUser(user).ToList();
            List<Indicator> indicators = new List<Indicator>();
            foreach (UserIndicator ui in userIndicator)
            {
                if (ui.user == user.ID) indicators.Add(iRepository.GetByID(ui.indicator));
            }
            return indicators;
        }

        public List<Indicator> GetAllIndicators(User user)
        {
            List<UserIndicator> toSearch = userIndicatorRepository.GetIndicatorsByUser(user);
            List<Indicator> toRetrieve = new List<Indicator>();
            IndicatorLogic il = new IndicatorLogic(null);
            foreach (UserIndicator uii in toSearch)
            {
                Indicator toAdd = (il.GetById(uii.indicator));
                if (uii.name == null)
                {
                    toRetrieve.Add(toAdd);
                }
                else
                {
                    toAdd.Name = uii.name;
                    toRetrieve.Add(toAdd);
                }
            }
            return toRetrieve;
        }

        public List<Indicator> GetVisibleIndicators(User user)
        {
            List<UserIndicator> toSearch = userIndicatorRepository.GetIndicatorsByUser(user);
            List<Indicator> toRetrieve = new List<Indicator>();
            IndicatorLogic il = new IndicatorLogic(null);
            foreach (UserIndicator uii in toSearch)
            {
                if (uii.visible)
                {
                    Indicator toAdd = (il.GetById(uii.indicator));
                    if (uii.name == null)
                    {
                        toRetrieve.Add(toAdd);
                    }
                    else
                    {
                        toAdd.Name = uii.name;
                        toRetrieve.Add(toAdd);
                    }
                }
            }
            return toRetrieve;
        }

        public List<Indicator> ReorderUserIndicators(User user, List<Indicator> indicators)
        {
            for (int i = 0; i < indicators.Count(); i++)
            {
                UserIndicator uii = userIndicatorRepository.Get(user, indicators.ElementAt(i));
                uii.order = i;
                userIndicatorRepository.Update(uii);
                userIndicatorRepository.Save();
            }
            List<Indicator> toRetrieve = this.GetAllIndicators(user);
            return toRetrieve;
        }

        public List<String> GetLoginUsersList()
        {
            List<String> users = new List<String>();
            List<Log> logs = logRepository.GetAll().ToList();

            foreach (Log log in logs)
            {
                if (log.Accion == "Login") users.Add(log.Username);
            }

            return users;
        }

        public List<Log> GetActionsBetweenDatesList(DateTime lowerDate, DateTime higherDate)
        {
            if (lowerDate > higherDate) throw new InvalidOperationException("Error en el orden de las fechas");
            if (lowerDate == null || higherDate == null) throw new NullReferenceException("Fecha no puede ser nula");
            List<Log> toReturn = new List<Log>();
            List<Log> logs = logRepository.GetAll().ToList();

            foreach (Log log in logs)
            {
                if (log.Date >= lowerDate && log.Date <= higherDate) toReturn.Add(log);
            }
            return toReturn;
        }


        public List<UserIndicator> GetAllHiddenIndicatros()
        {
            List<String> indicators = new List<String>();
            List<UserIndicator> uis = userIndicatorRepository.GetByCondition(x => x.visible == false).ToList();

            return uis;
        }

    }
}
