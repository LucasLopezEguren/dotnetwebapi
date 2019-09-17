using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CompanyDashboard.Domain;
using CompanyDashboard.BusinessLogic.Interface;
using CompanyDashboard.BusinessLogic;
using CompanyDashboard.WebApi.Models;
using CompanyDashboard.WebApi.Filters;

namespace CompanyDashboard.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase, IDisposable
    {
        private ISessionLogic sessionLogic;
        private IUserLogic userLogic;

        public SessionController(ISessionLogic sessionsLogic, IUserLogic userLogic)
        {
            if (sessionsLogic == null)
            {
                this.sessionLogic = new SessionLogic(null, null);
                this.userLogic = new UserLogic(null);
            }
            else
            {
                this.sessionLogic = sessionsLogic;
                this.userLogic = userLogic;
            }
        }

        [HttpPost]
        public IActionResult Login([FromBody] SessionModel sessionModel)
        {
            try
            {
                Guid token = sessionLogic.Login(sessionModel.username, sessionModel.password);
                if (token == null)
                {
                    return BadRequest(sessionModel);
                }
                LogLogic ll = new LogLogic(null);
                Log log = new Log{
                    Username = sessionModel.username,
                    Date = DateTime.Now,
                    Accion = "Login"
                };
                ll.AddRegister(log);
                return Ok(token);
            }
            catch (ArgumentException exception)
            {
                return BadRequest("Error "+ sessionModel.username + " " + sessionModel.password + " -> " + exception.Message);
            }
        }

        public void Dispose()
        {
            sessionLogic.Dispose();
        }
    }
}
