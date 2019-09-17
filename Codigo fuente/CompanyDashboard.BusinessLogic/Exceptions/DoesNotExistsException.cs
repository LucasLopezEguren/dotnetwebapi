using System;

namespace CompanyDashboard.BusinessLogic.Exceptions
{
    public class DoesNotExistsException : Exception
    {
        public DoesNotExistsException()
        {
        }
        public DoesNotExistsException(String message) : base(message)
        {
        }
    }
}