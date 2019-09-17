using System;

namespace CompanyDashboard.BusinessLogic.Exceptions
{
    public class NotValidException : Exception
    {
        public NotValidException()
        {
        }
        public NotValidException(String message) : base(message)
        {
        }
    }
}