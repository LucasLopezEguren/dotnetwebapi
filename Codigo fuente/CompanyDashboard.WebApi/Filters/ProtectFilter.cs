using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using CompanyDashboard.BusinessLogic;

namespace CompanyDashboard.WebApi.Filters {
    public class ProtectFilter : Attribute, IActionFilter
    {
        private readonly string role;

        public ProtectFilter(string role) 
        {
            this.role = role;           
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Obtenemos el token del header HTTP `authorization`
            string headerToken = context.HttpContext.Request.Headers["Authorization"];

            // Si el token es null, el usuario no se esta autenticado. Por eso cortamos
            // el pipeline. Si no envio un token, no es necesario seguir ejecutando el resto
            // de la aplicación.

            if (headerToken == null) {
                context.Result = new ContentResult()
                {
                    Content = "Debe iniciar sesión",
                };
            } else {
                try {
                    Guid token = Guid.Parse(headerToken);
                    VerifyToken(token, context);
                } catch (FormatException exception) {
                    context.Result = new ContentResult()
                    {
                        Content = "Token incorrecto. Debe iniciar sesión",
                    };
                }
            }
        }

        private void VerifyToken(Guid token, ActionExecutingContext context)
        {
            // Usamos using asi nos aseguramos que se llame el Dispose de este `sessions` enseguida salgamos del bloque
            using (var sessions = new SessionLogic(null, null)) {
                // Verificamos que el token sea valido
                if (!sessions.IsValidToken(token)) {
                    context.Result = new ContentResult()
                    {
                        Content = "Debe iniciar sesión para acceder",
                    };
                }
                // Verificamos que el rol del usuario sea correcto
                if (!sessions.HasLevel(token, role)) {
                    context.Result = new ContentResult()
                    {
                        Content = "No tiene permiso para acceder",
                    };   
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Vacio, ya que no queremos hacer nada despues de la request
        }
    }
}