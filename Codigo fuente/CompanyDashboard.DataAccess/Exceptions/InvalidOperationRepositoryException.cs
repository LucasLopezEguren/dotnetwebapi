using System;

namespace CompanyDashboard.DataAccess.Exceptions
{
    public class InvalidOperationRepositoryException : Exception
    {
        public InvalidOperationRepositoryException()
        {
        }
        public InvalidOperationRepositoryException(String message) : base(message)
        {
        }
    }
}