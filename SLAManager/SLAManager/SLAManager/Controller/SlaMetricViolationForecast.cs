using SLAManagerdata.ViewModels;
using Microsoft.AspNetCore.Mvc;
using SLAManager.Utils;
using SLAManager.Model;
using Microsoft.AspNetCore.Authorization;

namespace ExposeAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SlaMetricViolationForecastController : ControllerBase
    {
        #region GET
        [HttpGet]
        [Route("Get")]
        [Authorize]
        public async Task<ActionResult> Get()
        {
            Logger log = new();
            log.LogAction("SlaMetricViolationForecastController  Get");
            List<SlaMetricViolationForecast> SlaMetricViolationForecasts = new List<SlaMetricViolationForecast>();
            if (User.Identity.IsAuthenticated)
            {
                var idUserClaim = User.FindFirst("Id");
                int partition = Convert.ToInt32(User.FindFirst("Partition").Value);
                if (idUserClaim != null && int.TryParse(idUserClaim.Value, out int idUser))
                {
                    //var result = await Kafka.Kafka.producer.ProduceRequest<string>("", MessageType.Request, MessageTag.GetSlaMetricViolationForecast, ExposeAPI.Configurations.config.configuration["topic_to_configuration"], partition);
                    //SlaMetricViolationForecasts = await Kafka.Kafka.consumer.ConsumeResponse<List<SlaMetricViolationForecast>>((int)result.Offset);
                    //return SlaMetricViolationForecasts != null ? Ok(SlaMetricViolationForecasts) : Problem(null, null, 401);
                }
            }
            else
            {
                return Problem(null, null, 401);
            }
            return SlaMetricViolationForecasts != null ? Ok(SlaMetricViolationForecasts) : Problem(null, null, 500);
        }
        #endregion

        //#region POST
        //[Authorize]
        //[HttpPost]
        //[Route("Add")]
        //public async Task<ActionResult> Create(SlaMetricViolationForecastCreateDTO newItemDTO)
        //{
        //    Logger log = new();
        //    log.LogAction("SlaMetricViolationForecastController  Create");
        //    string res = null;
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        var idUserClaim = User.FindFirst("Id");
        //        int partition = Convert.ToInt32(User.FindFirst("Partition").Value);
        //        if (idUserClaim != null && int.TryParse(idUserClaim.Value, out int idUser))
        //        {
        //            var result=await Kafka.Kafka.producer.ProduceRequest<string>(newItemDTO,MessageType.Request,MessageTag.AddSlaMetricViolationForecast, ExposeAPI.Configurations.config.configuration["topic_to_configuration"], partition);
        //            res=await Kafka.Kafka.consumer.ConsumeResponse<string>((int)result.Offset);
        //            return res!=null ? Ok(res) : Problem(null, null, 401);
        //        }
        //    }
        //    else
        //    {
        //        return Problem(null, null, 401);
        //    }
        //    return res != null ? Ok(res) : Problem(null, null, 500);
        //}
        //#endregion

        //#region PATCH
        //[HttpPatch]
        //[Route("Patch")]
        //[Authorize]
        //public async Task<ActionResult> Patch(SlaMetricViolationForecastPatchDTO newItemDTO)
        //{
        //    Logger log = new();
        //    log.LogAction("SlaMetricViolationForecastController  Patch");
        //    string res = null;
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        var idUserClaim = User.FindFirst("Id");
        //        int partition = Convert.ToInt32(User.FindFirst("Partition").Value);
        //        if (idUserClaim != null && int.TryParse(idUserClaim.Value, out int idUser))
        //        {
        //            var result = await Kafka.Kafka.producer.ProduceRequest<string>(newItemDTO, MessageType.Request, MessageTag.PatchSlaMetricViolationForecast, ExposeAPI.Configurations.config.configuration["topic_to_configuration"], partition);
        //            res = await Kafka.Kafka.consumer.ConsumeResponse<string>((int)result.Offset);
        //            return res!=null ? Ok(res) : Problem(null, null, 401);
        //        }
        //    }
        //    else
        //    {
        //        return Problem(null, null, 401);
        //    }
        //    return res != null ? Ok(res) : Problem(null, null, 500);
        //}
        //#endregion

        //#region DELETE
        //[HttpDelete]
        //[Route("Delete")]
        //[Authorize]
        //public async Task<ActionResult> Delete( int? IdSlaMetricViolationForecast)
        //{
        //    Logger log = new();
        //    log.LogAction("SlaMetricViolationForecastController  Delete");
        //    string isDeleted = null;
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        var idUserClaim = User.FindFirst("Id");
        //        int partition = Convert.ToInt32(User.FindFirst("Partition").Value);
        //        if (idUserClaim != null && int.TryParse(idUserClaim.Value, out int idUser))
        //        {
        //            var deleteItemDTO = new
        //            {
        //                IdSlaMetricViolationForecast=IdSlaMetricViolationForecast,
        //            };
        //            var result = await Kafka.Kafka.producer.ProduceRequest<string>(deleteItemDTO, MessageType.Request, MessageTag.DeleteSlaMetricViolationForecast, ExposeAPI.Configurations.config.configuration["topic_to_configuration"], partition);
        //            isDeleted = await Kafka.Kafka.consumer.ConsumeResponse<string>((int)result.Offset);
        //            return isDeleted!=null ? Ok(isDeleted) : Problem(null, null,  401);
        //        }
        //    }
        //    else
        //    {
        //        return Problem(null, null, 401);
        //    }
        //    return isDeleted != null ? Ok(isDeleted) : Problem(null, null, 500);
        //}
        //#endregion
    }
}