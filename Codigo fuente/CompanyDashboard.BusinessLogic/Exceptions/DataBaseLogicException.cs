using System;

namespace CompanyDashboard.BusinessLogic.Exceptions
{
    public class DataBaseLogicException : Exception
    {
        public DataBaseLogicException()
        {
        }
        public DataBaseLogicException(String message) : base(message)
        {
        }
    }
}