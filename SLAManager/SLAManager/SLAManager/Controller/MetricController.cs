﻿using SLAManagerdata.ViewModels;
using Microsoft.AspNetCore.Mvc;
using SLAManager.Utils;
using SLAManager.Model;
using Microsoft.AspNetCore.Authorization;

namespace SLAManager.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MonitoringMetricController : ControllerBase
    {
        #region GET
        [HttpGet]
        [Route("Get")]
        [Authorize]
        public async Task<ActionResult> Get()
        {
            Logger log = new();
            log.LogAction("MonitoringMetricController  Get");
            List<MonitoringMetric> MonitoringMetrics = new List<MonitoringMetric>();
            if (User.Identity.IsAuthenticated)
            {
                var idUserClaim = User.FindFirst("Id");
                int partition = Convert.ToInt32(User.FindFirst("Partition").Value);
                if (idUserClaim != null && int.TryParse(idUserClaim.Value, out int idUser))
                {
                    //var result = await Kafka.Kafka.producer.ProduceRequest<string>("", MessageType.Request, MessageTag.GetMonitoringMetric, ExposeAPI.Configurations.config.configuration["topic_to_configuration"], partition);
                    //MonitoringMetrics = await Kafka.Kafka.consumer.ConsumeResponse<List<MonitoringMetric>>((int)result.Offset);
                    //return MonitoringMetrics != null ? Ok(MonitoringMetrics) : Problem(null, null, 401);
                }
            }
            else
            {
                return Problem(null, null, 401);
            }
            return MonitoringMetrics != null ? Ok(MonitoringMetrics) : Problem(null, null, 500);
        }
        #endregion

        //#region POST
        //[Authorize]
        //[HttpPost]
        //[Route("Add")]
        //public async Task<ActionResult> Create(MonitoringMetricCreateDTO newItemDTO)
        //{
        //    Logger log = new();
        //    log.LogAction("MonitoringMetricController  Create");
        //    string res = null;
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        var idUserClaim = User.FindFirst("Id");
        //        int partition = Convert.ToInt32(User.FindFirst("Partition").Value);
        //        if (idUserClaim != null && int.TryParse(idUserClaim.Value, out int idUser))
        //        {
        //            var result=await Kafka.Kafka.producer.ProduceRequest<string>(newItemDTO,MessageType.Request,MessageTag.AddMonitoringMetric, ExposeAPI.Configurations.config.configuration["topic_to_configuration"], partition);
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
        //public async Task<ActionResult> Patch(MonitoringMetricPatchDTO newItemDTO)
        //{
        //    Logger log = new();
        //    log.LogAction("MonitoringMetricController  Patch");
        //    string res = null;
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        var idUserClaim = User.FindFirst("Id");
        //        int partition = Convert.ToInt32(User.FindFirst("Partition").Value);
        //        if (idUserClaim != null && int.TryParse(idUserClaim.Value, out int idUser))
        //        {
        //            var result = await Kafka.Kafka.producer.ProduceRequest<string>(newItemDTO, MessageType.Request, MessageTag.PatchMonitoringMetric, ExposeAPI.Configurations.config.configuration["topic_to_configuration"], partition);
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
        //public async Task<ActionResult> Delete( int? IdMonitoringMetric)
        //{
        //    Logger log = new();
        //    log.LogAction("MonitoringMetricController  Delete");
        //    string isDeleted = null;
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        var idUserClaim = User.FindFirst("Id");
        //        int partition = Convert.ToInt32(User.FindFirst("Partition").Value);
        //        if (idUserClaim != null && int.TryParse(idUserClaim.Value, out int idUser))
        //        {
        //            var deleteItemDTO = new
        //            {
        //                IdMonitoringMetric=IdMonitoringMetric,
        //            };
        //            var result = await Kafka.Kafka.producer.ProduceRequest<string>(deleteItemDTO, MessageType.Request, MessageTag.DeleteMonitoringMetric, ExposeAPI.Configurations.config.configuration["topic_to_configuration"], partition);
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