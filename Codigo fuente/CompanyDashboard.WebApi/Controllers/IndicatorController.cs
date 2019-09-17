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
    public class IndicatorController : ControllerBase
    {
        private IIndicatorLogic indicatorLogic;
        private Indicator indicator;

        public IndicatorController(IIndicatorLogic otherIndicatorLogic)
        {
            if (otherIndicatorLogic == null)
            {
                indicatorLogic = new IndicatorLogic(null);
            }
            else
            {
                indicatorLogic = otherIndicatorLogic;
            }
        }

        [ProtectFilter("Admin")]
        [HttpGet]
        public ActionResult<List<Indicator>> Get()
        {
            try
            {
                var indicators = indicatorLogic.GetAllIndicators();
                if (indicators == null)
                {
                    return NotFound();
                }
                return Ok(indicators);
            }
            catch (DataBaseLogicException) { return BadRequest("Error en la conexión con la base de datos"); }
            catch (InvalidOperationLogicException) { return BadRequest("Error en el sistema"); }
        }

        [HttpGet("{id}/evaluation")]
        public ActionResult<String> GetEvaluation(int id, String color)
        {
            try
            {
                var indicator = indicatorLogic.GetById(id);
                if (indicator == null)
                {
                    return NotFound();
                }
                NodeLogic nl = new NodeLogic(null);
                List<String> evaluation = new List<string>();
                evaluation.Add(nl.GetText(indicator.Green));  
                evaluation.Add(nl.Evaluate(indicator.Green)+"");
                evaluation.Add(nl.GetText(indicator.Yellow));  
                evaluation.Add(nl.Evaluate(indicator.Yellow)+"");
                evaluation.Add(nl.GetText(indicator.Red));  
                evaluation.Add(nl.Evaluate(indicator.Red)+"");
                return Ok(evaluation);
                
            }
            catch (NullException) { return BadRequest("No es posible obtener un indicador nulo"); }
            catch (NotFoundException) { return BadRequest("No fue posible obtener ese indicador"); }
            catch (NullReferenceException) { return BadRequest("No es posible obtener un indicador nulo"); }
            catch (NotValidException) { return BadRequest("No es posible obtener un indicador no válido"); }
            catch (DataBaseLogicException) { return BadRequest("Error en la conexión con la base de datos"); }
            catch (InvalidOperationLogicException) { return BadRequest("Error en el sistema"); }
        }
        
        [ProtectFilter("Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            indicatorLogic.DeleteIndicator(id);
            return NoContent();
        }

        private Indicator IndicatorFromModel(Indicator indicator, IndicatorModel model)
        {
            if (model.Name != null) indicator.Name = model.Name;
            if (model.Green != null) indicator.Green = model.Green;
            if (model.Red != null) indicator.Red = model.Red;
            if (model.Yellow != null) indicator.Yellow = model.Yellow;

            return indicator;
        }
    }
}
