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
using CompanyDashboard.BusinessLogic.Exceptions;

namespace CompanyDashboard.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserLogic userLogic;
        private IIndicatorLogic indicatorLogic;

        public UserController(IUserLogic otherUserLogic)
        {
            if (otherUserLogic == null)
            {
                indicatorLogic = new IndicatorLogic(null);
                userLogic = new UserLogic(null);
            }
            else
            {
                indicatorLogic = new IndicatorLogic(null);
                userLogic = otherUserLogic;
            }
        }

        //Devolver todos los usuarios
        [HttpGet]
        public ActionResult<List<User>> Get()
        {
            try
            {
                var users = userLogic.GetAllUsers();
                if (users == null)
                {
                    return NotFound();
                }
                return Ok(users);
            }

            catch (DataBaseLogicException) { return BadRequest("Error en la conexión con la base de datos"); }
            catch (InvalidOperationLogicException) { return BadRequest("Error en el sistema"); }
        }

        

        //Devolver todos los indicadores visibles del usuario(id)
        [ProtectFilter("Manager")]
        [HttpGet("{id}/visibleIndicators")]
        public ActionResult<string> GetVisibleIndicators(int id)
        {
            try
            {
                var user = userLogic.GetUserByID(id);
                List<Indicator> indicadores = userLogic.GetVisibleIndicators(user);
                return Ok(indicadores);
            }
            catch (NullException) { return BadRequest("No es posible obtener un usuario nulo"); }
            catch (NotFoundException) { return BadRequest("No fue posible obtener ese usuario"); }
            catch (NullReferenceException) { return BadRequest("No es posible obtener un usuario nulo"); }
            catch (NotValidException) { return BadRequest("No es posible obtener un usuario no válido"); }
            catch (DataBaseLogicException) { return BadRequest("Error en la conexión con la base de datos"); }
            catch (InvalidOperationLogicException) { return BadRequest("Error en el sistema"); }
        }

        //[ProtectFilter("Manager")]
        [HttpGet("{id}/indicators")]
        public ActionResult<string> GetAllIndicators(int id)
        {
            try
            {
                var user = userLogic.GetUserByID(id);
                List<Indicator> indicadores = userLogic.GetAllIndicators(user);
                return Ok(indicadores);
            }
            catch (NullException) { return BadRequest("No es posible obtener un usuario nulo"); }
            catch (NotFoundException) { return BadRequest("No fue posible obtener ese usuario"); }
            catch (NullReferenceException) { return BadRequest("No es posible obtener un usuario nulo"); }
            catch (NotValidException) { return BadRequest("No es posible obtener un usuario no válido"); }
            catch (DataBaseLogicException) { return BadRequest("Error en la conexión con la base de datos"); }
            catch (InvalidOperationLogicException) { return BadRequest("Error en el sistema"); }
        }

        
        [HttpGet("{username}")]
        public ActionResult<string> Get(string username)
        {
            try
            {
                var user = userLogic.GetUserByName(username);
                return Ok(user);
            }
            catch (NullException) { return BadRequest("No es posible obtener un usuario nulo"); }
            catch (NotFoundException) { return BadRequest("No fue posible obtener ese usuario"); }
            catch (NullReferenceException) { return BadRequest("No es posible obtener un usuario nulo"); }
            catch (NotValidException) { return BadRequest("No es posible obtener un usuario no válido"); }
            catch (DataBaseLogicException) { return BadRequest("Error en la conexión con la base de datos"); }
            catch (InvalidOperationLogicException) { return BadRequest("Error en el sistema"); }
        }        

        [ProtectFilter("Admin")]
        [HttpGet("reporteAcciones")]
        public ActionResult<string> GetActionsBetweenDatesList([FromBody]DateData dateData)
        {
            try
            {
                List<Log> toReturn = userLogic.GetActionsBetweenDatesList(dateData.lowerDate, dateData.higherDate);
                return Ok(toReturn);
            }
            catch (InvalidOperationException) { return BadRequest("Error en el orden de las fechas"); }
            catch (NullReferenceException) { return BadRequest("Fecha no puede ser nula"); }
        }
       //Clase auxiliar (Nested class) para poder pasar 2 DateTime por parametro en el controller "reporteAcciones"
        public class DateData
        {
            public DateTime lowerDate { get; set; }
            public DateTime higherDate { get; set; }
        }



        //Devuelve todos los indicadores del usuario(id)
        [HttpGet("{id}/indicator/{color}")]
        public ActionResult<int> GetIndicators(int id, String color)
        {
            try
            {
                var user = userLogic.GetUserByID(id);
                List<Indicator> indicators = userLogic.GetAllIndicators(user);
                List<Indicator> toReturn = new List<Indicator>();
                NodeLogic nodeLogic = new NodeLogic(null);
                foreach (Indicator indicator in indicators)
                {
                    if (color.ToLowerInvariant() == "green" && nodeLogic.Evaluate(indicator.Green))
                    {
                        toReturn.Add(indicator);
                    }
                    if (color.ToLowerInvariant() == "red" && nodeLogic.Evaluate(indicator.Red))
                    {
                        toReturn.Add(indicator);
                    }
                    if (color.ToLowerInvariant() == "yellow" && nodeLogic.Evaluate(indicator.Yellow))
                    {
                        toReturn.Add(indicator);
                    }
                    if (color.ToLowerInvariant() == "all")
                    {
                        toReturn.Add(indicator);
                    }
                }
                return Ok(toReturn.Count);
            }
            catch (NullException) { return BadRequest("No es posible obtener un usuario nulo"); }
            catch (NotFoundException) { return BadRequest("No fue posible obtener ese usuario"); }
            catch (NullReferenceException) { return BadRequest("No es posible obtener un usuario nulo"); }
            catch (NotValidException) { return BadRequest("No es posible obtener un usuario no válido"); }
            catch (DataBaseLogicException) { return BadRequest("Error en la conexión con la base de datos"); }
            catch (InvalidOperationLogicException) { return BadRequest("Error en el sistema"); }
        }

        [ProtectFilter("Manager")]
        [HttpPut("{id}/indicator")]
        public ActionResult Put(int id, [FromBody]List<int> order)
        {
            try
            {
                var user = userLogic.GetUserByID(id);
                List<Indicator> indicadores = userLogic.GetAllIndicators(user);
                List<Indicator> orderSet = new List<Indicator>();
                IndicatorLogic il = new IndicatorLogic(null);
                foreach (int i in order)
                {
                    foreach (Indicator ind in indicadores)
                    {
                        if (ind.ID == i)
                        {
                            orderSet.Add(ind);
                        }
                    }
                }
                List<Indicator> toReturn = userLogic.ReorderUserIndicators(user, orderSet);
                return Ok(toReturn);
            }
            catch (NullException) { return BadRequest("No es posible obtener un usuario nulo"); }
            catch (NotFoundException) { return BadRequest("No fue posible obtener ese usuario"); }
            catch (NullReferenceException) { return BadRequest("No es posible obtener un usuario nulo"); }
            catch (NotValidException) { return BadRequest("No es posible obtener un usuario no válido"); }
            catch (DataBaseLogicException) { return BadRequest("Error en la conexión con la base de datos"); }
            catch (InvalidOperationLogicException) { return BadRequest("Error en el sistema"); }
        }

        [HttpPut("{id}/indicator/{idindicator}")]
        public IActionResult RenameIndicator(int id, int idindicator, [FromBody]String name)
        {
            try
            {
                userLogic.RenameIndicator(id, idindicator, name);
                return Ok("El indicador " + idindicator + " ha sido renombrado.");
            }
            catch (NullException) { return BadRequest("No es posible obtener un usuario o indicador nulo"); }
            catch (NotFoundException) { return BadRequest("No fue posible obtener ese usuario o indicador"); }
            catch (NullReferenceException) { return BadRequest("No es posible obtener un usuario o indicador nulo"); }
            catch (NotValidException) { return BadRequest("No es posible obtener un usuario no o indicador válido"); }
            catch (DataBaseLogicException) { return BadRequest("Error en la conexión con la base de datos"); }
            catch (InvalidOperationLogicException) { return BadRequest("Error en el sistema"); }
        }

        [ProtectFilter("Admin")]
        [HttpGet("reporteUsuarios")]
        public ActionResult<string> GetTop10Users()
        {
            try
            {
                var users = userLogic.GetLoginUsersList();
                var counts = users.GroupBy(x => x)
                .Select(g => new { Usuario = g.Key, Ingresos = g.Count() });
                counts.OrderByDescending(x => x.Ingresos).Take(10);

                return Ok(counts);
            }
            catch (NullException) { return BadRequest("No hay usuarios que hayan iniciado sesion"); }
            catch (NotFoundException) { return BadRequest("No hay usuarios que hayan iniciado sesion"); }
            catch (NullReferenceException) { return BadRequest("No hay usuarios que hayan iniciado sesion"); }
            catch (NotValidException) { return BadRequest("No hay usuarios que hayan iniciado sesion"); }
            catch (DataBaseLogicException) { return BadRequest("Error en la conexión con la base de datos"); }
            catch (InvalidOperationLogicException) { return BadRequest("Error en el sistema"); }
        }

        [ProtectFilter("Admin")]
        [HttpGet("reporteIndicadores")]
        public ActionResult<string> GetTop10HiddenIndicators()
        {
            try
            {
                var uis = userLogic.GetAllHiddenIndicatros();
                var counts = uis.GroupBy(x => x.indicator)
                .Select(g => new { Indicador = g.Key, Escondido = g.Count() });
                counts.OrderByDescending(x => x.Escondido).Take(10);

                return Ok(counts);
            }
            catch (NullException) { return BadRequest("No es posible obtener un usuario nulo"); }
            catch (NotFoundException) { return BadRequest("No fue posible obtener ese usuario"); }
            catch (NullReferenceException) { return BadRequest("No es posible obtener un usuario nulo"); }
            catch (NotValidException) { return BadRequest("No es posible obtener un usuario no válido"); }
            catch (DataBaseLogicException) { return BadRequest("Error en la conexión con la base de datos"); }
            catch (InvalidOperationLogicException) { return BadRequest("Error en el sistema"); }
        }

        //Agrega usuario desde el modelo
        //      [ProtectFilter("Admin")]
        [HttpPost]
        public IActionResult Post([FromBody]UserModel model)
        {
            try
            {
                var user = UserModel.ToEntity(model);
                var toReturn = userLogic.AddUser(user);
                return Ok("Se agregó el usuario " + user.Username + " con el ID " + toReturn.ID);
            }
            catch (AlreadyExistsException) { return BadRequest("No es posible agregar un usuario ya existente"); }
            catch (NullException) { return BadRequest("No es posible agregar un usuario nulo"); }
            catch (NullReferenceException) { return BadRequest("No es posible agregar un usuario nulo"); }
            catch (NotValidException) { return BadRequest("No es posible agregar un usuario no válido"); }
            catch (DataBaseLogicException) { return BadRequest("Error en la conexión con la base de datos"); }
            catch (InvalidOperationLogicException) { return BadRequest("Error en el sistema"); }
        }

        //Actualiza un usuario(id)
        [ProtectFilter("Admin")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]UserModel model)
        {
            try
            {
                User user = userLogic.GetUserByID(id);
                User userToUpdate = UserFromModel(user, model);
                userToUpdate.ID = id;
                userLogic.UpdateUser(userToUpdate);
                return Ok("Usuario con ID " + user.ID + " actualizado correctamente");
            }
            catch (NotFoundException) { return BadRequest("No fue posible obtener ese usuario"); }
            catch (NullException) { return BadRequest("No es posible actualizar un usuario nulo"); }
            catch (NullReferenceException) { return BadRequest("No es posible actualizar un usuario nulo"); }
            catch (DataBaseLogicException) { return BadRequest("Error en la conexión con la base de datos"); }
            catch (InvalidOperationLogicException) { return BadRequest("Error en el sistema"); }
        }
        //  /user/{id}/indicator

        [ProtectFilter("Manager")]
        [HttpPut("{id}/hideIndicator/{idindicator}")]
        public IActionResult HideIndicator(int id, int idindicator)
        {
            try
            {
                User user = userLogic.GetUserByID(id);
                Indicator indicator = indicatorLogic.GetById(idindicator);
                userLogic.HideIndicator(user, indicator);
                return Ok("Indicador con ID " + indicator.ID + " escondido correctamente");
            }
            catch (NotFoundException) { return BadRequest("No fue posible obtener ese indicador"); }
            catch (NullException) { return BadRequest("No es posible actualizar un indicador nulo"); }
            catch (NullReferenceException) { return BadRequest("No es posible actualizar un indicador nulo"); }
            catch (DataBaseLogicException) { return BadRequest("Error en la conexión con la base de datos"); }
            catch (InvalidOperationLogicException) { return BadRequest("Error en el sistema"); }
        }

        [ProtectFilter("Manager")]
        [HttpPut("{id}/showIndicator/{idindicator}")]
        public IActionResult ShowIndicator(int id, int idindicator)
        {
            try
            {
                User user = userLogic.GetUserByID(id);
                Indicator indicator = indicatorLogic.GetById(idindicator);
                userLogic.ShowIndicator(user, indicator);
                return Ok("Indicador con ID " + indicator.ID + " se muestra correctamente");
            }
            catch (NotFoundException) { return BadRequest("No fue posible obtener ese indicador"); }
            catch (NullException) { return BadRequest("No es posible actualizar un indicador nulo"); }
            catch (NullReferenceException) { return BadRequest("No es posible actualizar un indicador nulo"); }
            catch (DataBaseLogicException) { return BadRequest("Error en la conexión con la base de datos"); }
            catch (InvalidOperationLogicException) { return BadRequest("Error en el sistema"); }
        }


        //Elimina un usuario(id)
        [ProtectFilter("Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                String user = userLogic.GetUserByID(id).Username;
                userLogic.DeleteUser(id);
                return Ok("Usuario " + user + " eliminada correctamente");
            }
            catch (NotFoundException) { return BadRequest("No fue posible obtener ese usuario"); }
            catch (NullException) { return BadRequest("No es posible eliminar un usuario nulo"); }
            catch (NullReferenceException) { return BadRequest("No es posible eliminar un usuario nulo"); }
            catch (DataBaseLogicException) { return BadRequest("Error en la conexión con la base de datos"); }
            catch (InvalidOperationLogicException) { return BadRequest("Error en el sistema"); }
        }

        private User UserFromModel(User user, UserModel model)
        {

            if (model.Name != null) user.Name = model.Name;
            if (model.Lastname != null) user.Lastname = model.Lastname;
            if (model.Username != null) user.Username = model.Username;
            if (model.Mail != null) user.Mail = model.Mail;
            if (model.Password != null) user.Password = model.Password;
            user.Admin = model.Admin;

            return user;
        }
    }
}
