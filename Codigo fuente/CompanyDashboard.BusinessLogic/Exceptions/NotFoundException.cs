using System;

namespace CompanyDashboard.BusinessLogic.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException()
        {
        }
        public NotFoundException(String message) : base(message)
        {
        }
    }
}