using Userdata.ViewModels;
using Microsoft.AspNetCore.Mvc;
using ExposeAPI.Utils;
using ExposeAPI.Model;
using Microsoft.AspNetCore.Authorization;

namespace ExposeAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MetricController : ControllerBase
    {
        #region GET
        [HttpGet]
        [Route("Get")]
        [Authorize]
        public async Task<ActionResult> Get()
        {
            Logger log = new();
            log.LogAction("MetricController  Get");
            List<Metric> Metrics = new List<Metric>();
            if (User.Identity.IsAuthenticated)
            {
                var idUserClaim = User.FindFirst("Id");
                int partition = Convert.ToInt32(User.FindFirst("Partition").Value);
                string cluster = User.FindFirst("Cluster").Value;
                if (idUserClaim != null && int.TryParse(idUserClaim.Value, out int idUser))
                {
                    var result = await Kafka.Kafka.producer.ProduceRequest<string>("", MessageType.Request, MessageTag.GetMetric,cluster, ExposeAPI.Configurations.config.configuration["topic_to_configuration"], partition);
                    Metrics = await Kafka.Kafka.consumer.ConsumeResponse<List<Metric>>((int)result.Offset,cluster);
                    return Metrics != null ? Ok(Metrics) : Problem(null, null, 401);
                }
            }
            else
            {
                return Problem(null, null, 401);
            }
            return Metrics != null ? Ok(Metrics) : Problem(null, null, 500);
        }
        #endregion

        //#region POST
        //[Authorize]
        //[HttpPost]
        //[Route("Add")]
        //public async Task<ActionResult> Create(MetricCreateDTO newItemDTO)
        //{
        //    Logger log = new();
        //    log.LogAction("MetricController  Create");
        //    string res = null;
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        var idUserClaim = User.FindFirst("Id");
        //        int partition = Convert.ToInt32(User.FindFirst("Partition").Value);
        //        if (idUserClaim != null && int.TryParse(idUserClaim.Value, out int idUser))
        //        {
        //            var result=await Kafka.Kafka.producer.ProduceRequest<string>(newItemDTO,MessageType.Request,MessageTag.AddMetric, ExposeAPI.Configurations.config.configuration["topic_to_configuration"], partition);
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
        //public async Task<ActionResult> Patch(MetricPatchDTO newItemDTO)
        //{
        //    Logger log = new();
        //    log.LogAction("MetricController  Patch");
        //    string res = null;
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        var idUserClaim = User.FindFirst("Id");
        //        int partition = Convert.ToInt32(User.FindFirst("Partition").Value);
        //        if (idUserClaim != null && int.TryParse(idUserClaim.Value, out int idUser))
        //        {
        //            var result = await Kafka.Kafka.producer.ProduceRequest<string>(newItemDTO, MessageType.Request, MessageTag.PatchMetric, ExposeAPI.Configurations.config.configuration["topic_to_configuration"], partition);
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
        //public async Task<ActionResult> Delete( int? IdMetric)
        //{
        //    Logger log = new();
        //    log.LogAction("MetricController  Delete");
        //    string isDeleted = null;
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        var idUserClaim = User.FindFirst("Id");
        //        int partition = Convert.ToInt32(User.FindFirst("Partition").Value);
        //        if (idUserClaim != null && int.TryParse(idUserClaim.Value, out int idUser))
        //        {
        //            var deleteItemDTO = new
        //            {
        //                IdMetric=IdMetric,
        //            };
        //            var result = await Kafka.Kafka.producer.ProduceRequest<string>(deleteItemDTO, MessageType.Request, MessageTag.DeleteMetric, ExposeAPI.Configurations.config.configuration["topic_to_configuration"], partition);
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