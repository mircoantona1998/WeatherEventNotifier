using Userdata.ViewModels;
using Microsoft.AspNetCore.Mvc;
using ExposeAPI.Utils;
using ExposeAPI.Model;
using Microsoft.AspNetCore.Authorization;
using Confluent.Kafka;
using ExposeAPI.Interface;

namespace ExposeAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FrequencyController : ControllerBase
    {
        #region GET
        [HttpGet]
        [Route("Get")]
        [Authorize]
        public async Task<ActionResult> Get()
        {
            Logger log = new();
            log.LogAction("FrequencyController  Get");
            List<Frequency> Frequencys = new List<Frequency>();
            if (User.Identity.IsAuthenticated)
            {
                var idUserClaim = User.FindFirst("Id");
                int partition = Convert.ToInt32(User.FindFirst("Partition").Value);
                string cluster = User.FindFirst("Cluster").Value;
                if (idUserClaim != null && int.TryParse(idUserClaim.Value, out int idUser))
                {
                    var result = await Kafka.Kafka.producer.ProduceRequest<string>("", MessageType.Request, MessageTag.GetFrequency,cluster, ExposeAPI.Configurations.config.configuration["topic_to_configuration"], partition);
                    Frequencys = await Kafka.Kafka.consumer.ConsumeResponse<List<Frequency>>((int)result.Offset, cluster, partition);
                    return Frequencys != null ? Ok(Frequencys) : Problem(null, null, 401);
                }
            }
            else
            {
                return Problem(null, null, 401);
            }
            return Frequencys != null ? Ok(Frequencys) : Problem(null, null, 500);
        }
    

    #endregion
    //#region POST
    //    [Authorize]
    //    [HttpPost]
    //    [Route("Add")]
    //    public async Task<ActionResult> Create(FrequencyCreateDTO newItemDTO)
    //    {
    //        Logger log = new();
    //        log.LogAction("FrequencyController  Create");
    //        string res = null;
    //        if (User.Identity.IsAuthenticated)
    //        {
    //            var idUserClaim = User.FindFirst("Id");
    //            int partition = Convert.ToInt32(User.FindFirst("Partition").Value);
    //            if (idUserClaim != null && int.TryParse(idUserClaim.Value, out int idUser))
    //            {
    //                var result=await Kafka.Kafka.producer.ProduceRequest<string>(newItemDTO,MessageType.Request,MessageTag.AddFrequency, ExposeAPI.Configurations.config.configuration["topic_to_configuration"], partition);
    //                res=await Kafka.Kafka.consumer.ConsumeResponse<string>((int)result.Offset);
    //                return res!=null ? Ok(res) : Problem(null, null, 401);
    //            }
    //        }
    //        else
    //        {
    //            return Problem(null, null, 401);
    //        }
    //        return res != null ? Ok(res) : Problem(null, null, 500);
    //    }

    //    #endregion

    //    #region PATCH
    //    [HttpPatch]
    //    [Route("Patch")]
    //    [Authorize]
    //    public async Task<ActionResult> Patch(FrequencyPatchDTO newItemDTO)
    //    {
    //        Logger log = new();
    //        log.LogAction("FrequencyController  Patch");
    //        string res = null;
    //        if (User.Identity.IsAuthenticated)
    //        {
    //            var idUserClaim = User.FindFirst("Id");
    //            int partition = Convert.ToInt32(User.FindFirst("Partition").Value);
    //            if (idUserClaim != null && int.TryParse(idUserClaim.Value, out int idUser))
    //            {
    //                var result = await Kafka.Kafka.producer.ProduceRequest<string>(newItemDTO, MessageType.Request, MessageTag.PatchFrequency, ExposeAPI.Configurations.config.configuration["topic_to_configuration"], partition);
    //                res = await Kafka.Kafka.consumer.ConsumeResponse<string>((int)result.Offset);
    //                return res != null? Ok(res) : Problem(null, null, 401);
    //            }
    //        }
    //        else
    //        {
    //            return Problem(null, null, 401);
    //        }
    //        return res != null ? Ok(res) : Problem(null, null, 500);
    //    }
    //    #endregion

    //    #region DELETE
    //    [HttpDelete]
    //    [Route("Delete")]
    //    [Authorize]
    //    public async Task<ActionResult> Delete( int? IdFrequency)
    //    {
    //        Logger log = new();
    //        log.LogAction("FrequencyController  Delete");
    //        string isDeleted = null;
    //        if (User.Identity.IsAuthenticated)
    //        {
    //            var idUserClaim = User.FindFirst("Id");
    //            int partition = Convert.ToInt32(User.FindFirst("Partition").Value);
    //            if (idUserClaim != null && int.TryParse(idUserClaim.Value, out int idUser))
    //            {
    //                var deleteItemDTO = new
    //                {
    //                    IdFrequency=IdFrequency,
    //                };
    //                var result = await Kafka.Kafka.producer.ProduceRequest<string>(deleteItemDTO, MessageType.Request, MessageTag.DeleteFrequency, ExposeAPI.Configurations.config.configuration["topic_to_configuration"], partition);
    //                isDeleted = await Kafka.Kafka.consumer.ConsumeResponse<string>((int)result.Offset);
    //                return isDeleted != null ? Ok(isDeleted) : Problem(null, null,  401);
    //            }
    //        }
    //        else
    //        {
    //            return Problem(null, null, 401);
    //        }
    //        return isDeleted != null ? Ok(isDeleted) : Problem(null, null, 500);
    //    }
    //    #endregion
    }
}