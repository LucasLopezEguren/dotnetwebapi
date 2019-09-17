using System;
using System.Collections.Generic;
using System.Linq;
using CompanyDashboard.Domain;

namespace CompanyDashboard.WebApi.Models
{
    public class SessionModel //: Model<Session, SessionModel>
    {
       public String username;
       public String password;

      /*  public SessionModel() { }

        public SessionModel(Session entity)
        {
            SetModel(entity);
        }

        public override Session ToEntity() => new Session()
        {
            
        };

        protected override SessionModel SetModel(Session entity)
        {
            return this;
        } */
    }
}
