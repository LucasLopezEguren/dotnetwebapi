using System;

namespace CompanyDashboard.BusinessLogic.Exceptions
{
    public class NullException : Exception
    {
        public NullException()
        {
        }
        public NullException(String message) : base(message)
        {
        }
    }
}