using System;

namespace CompanyDashboard.BusinessLogic.Exceptions
{
    public class AlreadyExistsException : Exception
    {
        public AlreadyExistsException()
        {
        }
        public AlreadyExistsException(String message) : base(message)
        {
        }
    }
}