using SLAManagerdata.ViewModels;
using Microsoft.AspNetCore.Mvc;
using SLAManager.Utils;
using SLAManager.Model;
using Microsoft.AspNetCore.Authorization;

namespace ExposeAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SlaMetricViolationController : ControllerBase
    {
        #region GET
        [HttpGet]
        [Route("Get")]
        [Authorize]
        public async Task<ActionResult> Get()
        {
            Logger log = new();
            log.LogAction("SlaMetricViolationController  Get");
            List<SlaMetricViolation> SlaMetricViolations = new List<SlaMetricViolation>();
            if (User.Identity.IsAuthenticated)
            {
                var idUserClaim = User.FindFirst("Id");
                int partition = Convert.ToInt32(User.FindFirst("Partition").Value);
                if (idUserClaim != null && int.TryParse(idUserClaim.Value, out int idUser))
                {
                    //var result = await Kafka.Kafka.producer.ProduceRequest<string>("", MessageType.Request, MessageTag.GetSlaMetricViolation, ExposeAPI.Configurations.config.configuration["topic_to_configuration"], partition);
                    //SlaMetricViolations = await Kafka.Kafka.consumer.ConsumeResponse<List<SlaMetricViolation>>((int)result.Offset);
                    //return SlaMetricViolations != null ? Ok(SlaMetricViolations) : Problem(null, null, 401);
                }
            }
            else
            {
                return Problem(null, null, 401);
            }
            return SlaMetricViolations != null ? Ok(SlaMetricViolations) : Problem(null, null, 500);
        }
        #endregion

        //#region POST
        //[Authorize]
        //[HttpPost]
        //[Route("Add")]
        //public async Task<ActionResult> Create(SlaMetricViolationCreateDTO newItemDTO)
        //{
        //    Logger log = new();
        //    log.LogAction("SlaMetricViolationController  Create");
        //    string res = null;
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        var idUserClaim = User.FindFirst("Id");
        //        int partition = Convert.ToInt32(User.FindFirst("Partition").Value);
        //        if (idUserClaim != null && int.TryParse(idUserClaim.Value, out int idUser))
        //        {
        //            var result=await Kafka.Kafka.producer.ProduceRequest<string>(newItemDTO,MessageType.Request,MessageTag.AddSlaMetricViolation, ExposeAPI.Configurations.config.configuration["topic_to_configuration"], partition);
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
        //public async Task<ActionResult> Patch(SlaMetricViolationPatchDTO newItemDTO)
        //{
        //    Logger log = new();
        //    log.LogAction("SlaMetricViolationController  Patch");
        //    string res = null;
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        var idUserClaim = User.FindFirst("Id");
        //        int partition = Convert.ToInt32(User.FindFirst("Partition").Value);
        //        if (idUserClaim != null && int.TryParse(idUserClaim.Value, out int idUser))
        //        {
        //            var result = await Kafka.Kafka.producer.ProduceRequest<string>(newItemDTO, MessageType.Request, MessageTag.PatchSlaMetricViolation, ExposeAPI.Configurations.config.configuration["topic_to_configuration"], partition);
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
        //public async Task<ActionResult> Delete( int? IdSlaMetricViolation)
        //{
        //    Logger log = new();
        //    log.LogAction("SlaMetricViolationController  Delete");
        //    string isDeleted = null;
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        var idUserClaim = User.FindFirst("Id");
        //        int partition = Convert.ToInt32(User.FindFirst("Partition").Value);
        //        if (idUserClaim != null && int.TryParse(idUserClaim.Value, out int idUser))
        //        {
        //            var deleteItemDTO = new
        //            {
        //                IdSlaMetricViolation=IdSlaMetricViolation,
        //            };
        //            var result = await Kafka.Kafka.producer.ProduceRequest<string>(deleteItemDTO, MessageType.Request, MessageTag.DeleteSlaMetricViolation, ExposeAPI.Configurations.config.configuration["topic_to_configuration"], partition);
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