using System;

namespace CompanyDashboard.DataAccess.Exceptions
{
    public class DataBaseRepositoryException : Exception
    {
        public DataBaseRepositoryException()
        {
        }
        public DataBaseRepositoryException(String message) : base(message)
        {
        }
    }
}