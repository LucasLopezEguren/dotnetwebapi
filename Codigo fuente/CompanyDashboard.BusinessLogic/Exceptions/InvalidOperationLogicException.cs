using System;

namespace CompanyDashboard.BusinessLogic.Exceptions
{
    public class InvalidOperationLogicException : Exception
    {
        public InvalidOperationLogicException()
        {
        }
        public InvalidOperationLogicException(String message) : base(message)
        {
        }
    }
}