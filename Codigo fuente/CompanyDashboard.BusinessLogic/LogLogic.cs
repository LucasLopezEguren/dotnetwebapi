using System;
using System.Linq;
using System.Collections.Generic;
using CompanyDashboard.BusinessLogic.Interface;
using CompanyDashboard.Domain;
using CompanyDashboard.DataAccess.Interface;
using CompanyDashboard.DataAccess;
using CompanyDashboard.BusinessLogic.Exceptions;
using System.Globalization;

namespace CompanyDashboard.BusinessLogic
{
    public class LogLogic : ILog
    {
        private IRepository<Log> repository;
        public LogLogic(IRepository<Log> repository)
        {
            if (repository == null)
            {
                this.repository = new LogRepository(ContextFactory.GetNewContext());
            }
            else
            {
                this.repository = repository;
            }
        }
    
        public Log AddRegister(Log log)
        {
            DateTime Date = DateTime.UtcNow;
            DateTime MvdeoDate = Date.AddHours(-3);
            log.Date = MvdeoDate;
            repository.Add(log);
            repository.Save();
            return log;
        }
    }
}
