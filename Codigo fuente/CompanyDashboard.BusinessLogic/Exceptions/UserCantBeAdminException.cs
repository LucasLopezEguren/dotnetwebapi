using System;

namespace CompanyDashboard.BusinessLogic.Exceptions
{
    public class UserCantBeAdminException : Exception
    {
        public UserCantBeAdminException()
        {
        }
        public UserCantBeAdminException(String message) : base(message)
        {
        }
    }
}