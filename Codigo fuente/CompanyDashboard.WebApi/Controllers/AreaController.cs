using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CompanyDashboard.Domain;
using CompanyDashboard.BusinessLogic.Interface;
using CompanyDashboard.BusinessLogic;
using CompanyDashboard.WebApi.Models;
using CompanyDashboard.BusinessLogic.Exceptions;
using CompanyDashboard.WebApi.Filters;

namespace CompanyDashboard.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        private IAreaLogic areaLogic;
        private IUserLogic userLogic;
        private IIndicatorLogic indicatorLogic;
        private IAreaUserLogic areaUserLogic;
        private ISessionLogic sessionLogic;
        private Area area;

        public AreaController(IAreaLogic otherAreaLogic)
        {
            if (otherAreaLogic == null)
            {
                areaLogic = new AreaLogic(null);
                userLogic = new UserLogic(null);
                indicatorLogic = new IndicatorLogic(null);
                sessionLogic = new SessionLogic(null, null);
            }
            else
            {
                userLogic = new UserLogic(null);
                areaLogic = otherAreaLogic;
                indicatorLogic = new IndicatorLogic(null);
                sessionLogic = new SessionLogic(null, null);
            }
        }

        //Devolver todas las areas
        [ProtectFilter("Admin")]
        [HttpGet]
        public ActionResult<List<Area>> Get()
        {
            try
            {
                var areas = areaLogic.GetAll();
                if (areas == null)
                {
                    return NotFound();
                }
                return Ok(areas);
            }

            catch (DataBaseLogicException) { return BadRequest("Error en la conexión con la base de datos"); }
            catch (InvalidOperationLogicException) { return BadRequest("Error en el sistema"); }
        }

        //Dado un area(id) se devuelven todos los indicadores de esa area
        [ProtectFilter("Admin")]
        [HttpGet("{id}/indicator")]
        public IActionResult GetAreaIndicators(int id)
        {
            try
            {
                Area area = areaLogic.GetAreaByID(id);
                var indicators = areaLogic.GetIndicators(area);
                return Ok(indicators);
            }
            catch (NullException) { return BadRequest("No es posible obtener de un area nula"); }
            catch (NullReferenceException) { return BadRequest("No es posible obtener de un area nula"); }
            catch (DataBaseLogicException) { return BadRequest("Error en la conexión con la base de datos"); }
            catch (InvalidOperationLogicException) { return BadRequest("Error en el sistema"); }
        }         

        //Devolver un area(id)
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            try
            {
                var area = areaLogic.GetAreaByID(id);
                return Ok(area);
            }
            catch (NullException) { return BadRequest("No es posible obtener un area nula"); }
            catch (NotFoundException) { return BadRequest("No fue posible obtener esa area"); }
            catch (NullReferenceException) { return BadRequest("No es posible obtener un area nula"); }
            catch (NotValidException) { return BadRequest("No es posible obtener un area no válida"); }
            catch (DataBaseLogicException) { return BadRequest("Error en la conexión con la base de datos"); }
            catch (InvalidOperationLogicException) { return BadRequest("Error en el sistema"); }
        }

        //Agrega area
        [ProtectFilter("Admin")]
        [HttpPost]
        public IActionResult Post([FromBody]AreaModel model)
        {
            try
            {
                var area = AreaModel.ToEntity(model);
                var toReturn = areaLogic.AddArea(area);
                return Ok("Se agregó el area " + area.Name + " con el ID " + toReturn.ID);
            }
            catch (AlreadyExistsException) { return BadRequest("No es posible agregar un area ya existente"); }
            catch (NullException) { return BadRequest("No es posible agregar un area nula"); }
            catch (NullReferenceException) { return BadRequest("No es posible agregar un area nula"); }
            catch (NotValidException) { return BadRequest("No es posible agregar un area no válida"); }
            catch (DataBaseLogicException) { return BadRequest("Error en la conexión con la base de datos"); }
            catch (InvalidOperationLogicException) { return BadRequest("Error en el sistema"); }
        }

        [ProtectFilter("Admin")]
        [HttpPost("{id}/import")]
        public IActionResult Import(Guid token)
        {
            try
            {
                LogLogic ll = new LogLogic(null);
                User user = sessionLogic.GetUserFromToken(token);
                Log log = new Log{
                    Username = user.Username,
                    Date = DateTime.Now,
                    Accion = "Import"
                };
                ll.AddRegister(log);
                return Ok();
            }
            catch (DataBaseLogicException) { return BadRequest("Error en la conexión con la base de datos"); }
            catch (InvalidOperationLogicException) { return BadRequest("Error en el sistema"); }
            
        }



        //Agregar a un area(id) un usuario (username)
        [ProtectFilter("Admin")]
        [HttpPost("{id}/{username}")]
        public IActionResult Post(int id, String username)
        {
            try
            {
                Area thisArea = areaLogic.GetAreaByID(id);
                User newUser = userLogic.GetUserByName(username);
                areaLogic.AddUser(thisArea, newUser);
                return Ok("Usuario " + username + " añadido al area " + thisArea.Name + " correctamente");
            }
            catch (AlreadyExistsException) { return BadRequest("No es posible agregar un usuario a un area ya existente"); }
            catch (NullException) { return BadRequest("No es posible agregar un area/usuario nulo"); }
            catch (NullReferenceException) { return BadRequest("No es posible agregar un usuario nulo"); }
            catch (NotValidException) { return BadRequest("No es posible agregar un usuario no válido"); }
            catch (DataBaseLogicException) { return BadRequest("Error en la conexión con la base de datos"); }
            catch (InvalidOperationLogicException) { return BadRequest("Error en el sistema"); }
        }

        //Eliminar de un area(id) un usuario (username)
        [ProtectFilter("Admin")]
        [HttpDelete("{id}/{username}")]
        public IActionResult Delete(int id, String username)
        {
            try
            {
                Area thisArea = areaLogic.GetAreaByID(id);
                User newUser = userLogic.GetUserByName(username);
                areaLogic.RemoveUser(thisArea, newUser);
                return Ok("Usuario " + username + " removido del area " + thisArea.Name + " correctamente");
            }
            catch (NotFoundException) { return BadRequest("No fue posible obtener esa area/usuario"); }
            catch (NullException) { return BadRequest("No es posible eliminar un area/usuario nulo"); }
            catch (NullReferenceException) { return BadRequest("No es posible eliminar un usuario nulo"); }
            catch (DataBaseLogicException) { return BadRequest("Error en la conexión con la base de datos"); }
            catch (InvalidOperationLogicException) { return BadRequest("Error en el sistema"); }
        }

        //Actualiza un area(id)
        [ProtectFilter("Admin")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]AreaModel model)
        {
            try
            {
                Area area = areaLogic.GetAreaByID(id);
                Area areaToUpdate = AreaFromModel(area, model);
                areaToUpdate.ID = id;
                areaLogic.UpdateArea(areaToUpdate);
                return Ok("Area con ID " + area.ID + " actualizada correctamente");
            }
            catch (NotFoundException) { return BadRequest("No fue posible obtener esa area"); }
            catch (NullException) { return BadRequest("No es posible actualizar un area nula"); }
            catch (NullReferenceException) { return BadRequest("No es posible actualizar un area nula"); }
            catch (DataBaseLogicException) { return BadRequest("Error en la conexión con la base de datos"); }
            catch (InvalidOperationLogicException) { return BadRequest("Error en el sistema"); }
        }

        //Elimina un area(id)
        [ProtectFilter("Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                String area = areaLogic.GetAreaByID(id).Name;
                areaLogic.DeleteArea(id);
                return Ok("Area " + area + " eliminada correctamente");
            }
            catch (NotFoundException) { return BadRequest("No fue posible obtener esa area"); }
            catch (NullException) { return BadRequest("No es posible eliminar un area nula"); }
            catch (NullReferenceException) { return BadRequest("No es posible eliminar un area nula"); }
            catch (DataBaseLogicException) { return BadRequest("Error en la conexión con la base de datos"); }
            catch (InvalidOperationLogicException) { return BadRequest("Error en el sistema"); }
        }

        //Agregar a un area(id) un indicator (indicator)
        [ProtectFilter("Admin")]
        [HttpPost("{id}/indicator")]
        public IActionResult Post(int id, [FromBody]IndicatorModel model)
        {
            try
            {
                var indicator = IndicatorModel.ToEntity(model);
                indicator.Area=id;
                var toReturn = indicatorLogic.AddIndicator(indicator);
                List<UserIndicator> ui = new List<UserIndicator>();

                return Ok("Se agregó el area " + indicator.Name + " con el ID " + toReturn.ID + " al area ");
            }
            catch (AlreadyExistsException) { return BadRequest("No es posible agregar un area ya existente"); }
            catch (NullException) { return BadRequest("No es posible agregar un area nula"); }
            catch (NullReferenceException) { return BadRequest("No es posible agregar un area nula"); }
            catch (NotValidException) { return BadRequest("No es posible agregar un area no válida"); }
            catch (DataBaseLogicException) { return BadRequest("Error en la conexión con la base de datos"); }
            catch (InvalidOperationLogicException) { return BadRequest("Error en el sistema"); }
        }
       

        private Area AreaFromModel(Area area, AreaModel model)
        {
            if (model.Name != null) area.Name = model.Name;
            if (model.DataSource != null) area.DataSource = model.DataSource;

            return area;
        }
    }
}
