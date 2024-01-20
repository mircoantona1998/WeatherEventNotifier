using SLAManagerdata.ViewModels;
using Microsoft.AspNetCore.Mvc;
using SLAManager.Utils;
using SLAManager.Model;
using Microsoft.AspNetCore.Authorization;


namespace SLAManager.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SlaController : ControllerBase
    {
        #region GET
        [HttpGet]
        [Route("Get")]
        [Authorize]
        public async Task<ActionResult> Get()
        {
            Logger log = new();
            log.LogAction("Sla controller  Get");
            List<Sla> Slas = null;
            if (User.Identity.IsAuthenticated)
            {
                var idUserClaim = User.FindFirst("Id");
                int partition = Convert.ToInt32(User.FindFirst("Partition").Value);
                if (idUserClaim != null && int.TryParse(idUserClaim.Value, out int idUser))
                {
                    var dto = new
                    {
                        IdUser = idUser
                    };
                    //var result = await Kafka.Kafka.producer.ProduceRequest<string>(dto, MessageType.Request, MessageTag.GetSla, SLAManager.Slas.config.Sla["topic_to_Sla"], partition);
                    //Slas = await Kafka.Kafka.consumer.ConsumeResponse<List<Sla>>((int)result.Offset);
                }
            }
            else
            {
                return Problem(null, null, 401);
            }
            return Slas != null ? Ok(Slas) : Problem(null, null, 500);
        }
        #endregion

        #region POST
        [Authorize]
        [HttpPost]
        [Route("Add")]
        public async Task<ActionResult> Create(SlaAddDTO newItemDTO)
        {
            Logger log = new();
            log.LogAction("SlaController  Create");
            string res=null;
            if (User.Identity.IsAuthenticated)
            {
                var idUserClaim = User.FindFirst("Id");
                int partition = Convert.ToInt32(User.FindFirst("Partition").Value);
                if (idUserClaim != null && int.TryParse(idUserClaim.Value, out int idUser))
                {
                    if (newItemDTO.Symbol == ">" || newItemDTO.Symbol == ">=" || newItemDTO.Symbol == "==" || newItemDTO.Symbol == "<" || newItemDTO.Symbol == "<=")
                    {
                        //var kafkaRequest = SlaCreateRequestKafka.ConvertSlaCreateToRequestKafka(newItemDTO, idUser);
                        //var result = await Kafka.Kafka.producer.ProduceRequest<string>(kafkaRequest, MessageType.Request, MessageTag.AddSla, SLAManager.Slas.config.Sla["topic_to_Sla"], partition);
                        //res = await Kafka.Kafka.consumer.ConsumeResponse<string>((int)result.Offset);
                    }else return Problem("Inserire un simbolo valido", null, 500);
                }
            }
            else
            {
                return Problem(null, null, 401);
            }
            return res != null ? Ok(res) : Problem(null, null, 500);
        }
        #endregion

        #region PATCH
        [HttpPatch]
        [Route("Patch")]
        [Authorize]
        public async Task<ActionResult> Patch(SlaPatchDTO newItemDTO)
        {
            Logger log = new();
            log.LogAction("SlaController  Patch");
            string res = null;
            if (User.Identity.IsAuthenticated)
            {
                var idUserClaim = User.FindFirst("Id");
                int partition = Convert.ToInt32(User.FindFirst("Partition").Value);
                if (idUserClaim != null && int.TryParse(idUserClaim.Value, out int idUser))
                {
                    if (newItemDTO.Symbol==null || newItemDTO.Symbol == ">" || newItemDTO.Symbol == ">=" || newItemDTO.Symbol == "==" || newItemDTO.Symbol == "<" || newItemDTO.Symbol == "<=")
                    {
                        //var kafkaRequest = SlaPatchRequestKafka.ConvertSlaPatchToRequestKafka(newItemDTO, idUser);
                        //var result = await Kafka.Kafka.producer.ProduceRequest<string>(kafkaRequest, MessageType.Request, MessageTag.PatchSla, SLAManager.Slas.config.Sla["topic_to_Sla"],partition);
                        //res = await Kafka.Kafka.consumer.ConsumeResponse<string>((int)result.Offset);
                    }
                    else return Problem("Inserire un simbolo valido", null, 500);
                }
            }
            else
            {
                return Problem(null, null, 401);
            }
            return res !=null? Ok(res) : Problem(null, null, 500);
        }
        #endregion

        #region DELETE
        [HttpDelete]
        [Route("Delete")]
        [Authorize]
        public async Task<ActionResult> Delete( int? IdSla)
        {
            Logger log = new();
            log.LogAction("SlaController  Delete");
            string res = null;
            if (User.Identity.IsAuthenticated)
            {
                var idUserClaim = User.FindFirst("Id");
                int partition = Convert.ToInt32(User.FindFirst("Partition").Value);
                if (idUserClaim != null && int.TryParse(idUserClaim.Value, out int idUser))
                {
                    var deleteItemDTO = new
                    {
                        IdUser = idUser,
                        IdSla = IdSla,
                    };
                    //var result = await Kafka.Kafka.producer.ProduceRequest<string>(deleteItemDTO, MessageType.Request, MessageTag.DeleteSla, SLAManager.Slas.config.Sla["topic_to_Sla"], partition);
                    //res = await Kafka.Kafka.consumer.ConsumeResponse<string>((int)result.Offset);
                }
            }
            else
            {
                return Problem(null, null, 401);
            }
            return res != null ? Ok(res) : Problem(null, null, 500);
        }
        #endregion
    }
}