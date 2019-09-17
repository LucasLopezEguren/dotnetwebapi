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
    public class SessionLogic : ISessionLogic
    {
        private IRepository<Session> sessionRepository;
        private IRepository<User> userRepository;

        public SessionLogic(IRepository<Session> sessionRepository, IRepository<User> userRepository)
        {
            if (sessionRepository == null || userRepository == null)
            {
                this.sessionRepository = new SessionRepository();
                this.userRepository = new UserRepository(ContextFactory.GetNewContext());
            }
            else
            {
                this.sessionRepository = sessionRepository;
                this.userRepository = userRepository;
            }
        }

        public User GetUserFromToken(Guid token)
        {
            Session sessionForToken = sessionRepository.GetFirst(s => s.Token == token);
            return userRepository.GetByName(sessionForToken.username);
        }
                        

        public Guid Login(string username, string password)
        {
            Guid sessionToken = Guid.NewGuid();
            User user = userRepository.GetByName(username);
            if (user == null)
            {
                throw new ArgumentException("Usuario o contraseña incorrecta");
            }
            else if (user.Password != password)
            {
                throw new ArgumentException("Usuario o contraseña incorrecta");
            }

            Session session = new Session() { Token = sessionToken, username = user.Username };
            sessionRepository.Add(session);
            sessionRepository.Save();

            return sessionToken;
        }

        public void Dispose()
        {
            sessionRepository.Dispose();
            userRepository.Dispose();
        }

        public bool IsValidToken(Guid token)
        {
            Session sessionForToken = sessionRepository.GetFirst(s => s.Token == token);
            return sessionForToken != null;
        }

        public bool HasLevel(Guid token, string role)
        {
            Session sessionForToken = sessionRepository.GetFirst(s => s.Token == token);
            User sessionUser = userRepository.GetByName(sessionForToken.username);

            if (sessionUser == null)
            {
                return false;
            }

            if (sessionUser.Admin && role == "Admin")
            {
                return true;
            }
            if (!sessionUser.Admin && role == "Manager")
            {
                return true;
            }

            return false;
        }

    }
}
