using Userdata.ViewModels;
using Microsoft.AspNetCore.Mvc;
using ExposeAPI.Utils;
using ExposeAPI.Model;
using Microsoft.AspNetCore.Authorization;

namespace ExposeAPI.Controllers
{    
    [Route("[controller]")]
    [ApiController]
    public class UserTelegramController : ControllerBase
    {
        #region GET
        [HttpGet]
        [Route("Get")]
        [Authorize]
        public async Task<ActionResult> Get(bool? all = null)
        {
            Logger log = new();
            log.LogAction("UserTelegramController  Get");
            List<UserTelegram> usertel = null;
            if (User.Identity.IsAuthenticated)
            {
                var idUserClaim = User.FindFirst("Id");
                int partition = Convert.ToInt32(User.FindFirst("Partition").Value);
                string cluster = User.FindFirst("Cluster").Value;
                if (idUserClaim != null && int.TryParse(idUserClaim.Value, out int idUser))
                {
                    var dto = new
                    {
                        IdUser = idUser,
                        All=all
                    };
                    var result = await Kafka.Kafka.producer.ProduceRequest<string>(dto, MessageType.Request, MessageTag.GetUserTelegram,cluster, ExposeAPI.Configurations.config.configuration["topic_to_telegram"], partition);
                    usertel = await Kafka.Kafka.consumer.ConsumeResponse<List<UserTelegram>>((int)result.Offset, cluster, partition);
                }
            }
            else
            {
                return Problem(null, null, 401);
            }
            return usertel != null ? Ok(usertel) : Problem(null, null, 500);
        }
        #endregion

        #region POST
        [Authorize]
        [HttpPost]
        [Route("Add")]
        public async Task<ActionResult> Create(UserTelegramCreateDTO newItemDTO)
        {
            Logger log = new();
            log.LogAction("UserTelegramController  Create");
            string res = null;
            if (User.Identity.IsAuthenticated)
            {
                var idUserClaim = User.FindFirst("Id");
                int partition = Convert.ToInt32(User.FindFirst("Partition").Value);
                string cluster = User.FindFirst("Cluster").Value;
                if (idUserClaim != null && int.TryParse(idUserClaim.Value, out int idUser))
                {
                    var kafkaRequest = TelegramCreateRequestKafka.ConvertTelegramCreateToRequestKafka(newItemDTO, idUser);
                    var result = await Kafka.Kafka.producer.ProduceRequest<string>(kafkaRequest, MessageType.Request, MessageTag.AddUserTelegram,cluster, ExposeAPI.Configurations.config.configuration["topic_to_telegram"], partition);
                    res = await Kafka.Kafka.consumer.ConsumeResponse<string>((int)result.Offset, cluster,partition);
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
        public async Task<ActionResult> Patch(UserTelegramPatchDTO newItemDTO)
        {
            Logger log = new();
            log.LogAction("UserTelegramController  Patch");
            string res = null;
            if (User.Identity.IsAuthenticated)
            {
                var idUserClaim = User.FindFirst("Id");
                int partition = Convert.ToInt32(User.FindFirst("Partition").Value);
                string cluster = User.FindFirst("Cluster").Value;
                if (idUserClaim != null && int.TryParse(idUserClaim.Value, out int idUser))
                {
                    var kafkaRequest = TelegramPatchRequestKafka.ConvertTelegramPatchToRequestKafka(newItemDTO, idUser);
                    var result = await Kafka.Kafka.producer.ProduceRequest<string>(kafkaRequest, MessageType.Request, MessageTag.PatchUserTelegram,cluster, ExposeAPI.Configurations.config.configuration["topic_to_telegram"], partition);
                    res = await Kafka.Kafka.consumer.ConsumeResponse<string>((int)result.Offset, cluster,partition);
                }
            }
            else
            {
                return Problem(null, null, 401);
            }
            return res != null ? Ok(res) : Problem(null, null, 500);
        }
        #endregion

        #region DELETE
        [HttpDelete]
        [Route("Delete")]
        [Authorize]
        public async Task<ActionResult> Delete( )
        {
            Logger log = new();
            log.LogAction("UserTelegramController  Delete");
            string isDeleted = null;
            if (User.Identity.IsAuthenticated)
            {
                var idUserClaim = User.FindFirst("Id");
                int partition = Convert.ToInt32(User.FindFirst("Partition").Value);
                string cluster = User.FindFirst("Cluster").Value;
                if (idUserClaim != null && int.TryParse(idUserClaim.Value, out int idUser))
                {
                    var dto = new
                    {
                        IdUser = idUser
                    };
                    var result = await Kafka.Kafka.producer.ProduceRequest<string>(dto, MessageType.Request, MessageTag.DeleteUserTelegram,cluster, ExposeAPI.Configurations.config.configuration["topic_to_telegram"], partition);
                    isDeleted = await Kafka.Kafka.consumer.ConsumeResponse<string>((int)result.Offset, cluster,partition);
                }
            }
            else
            {
                return Problem(null, null, 401);
            }
            return isDeleted != null ? Ok(isDeleted) : Problem(null, null, 500);
        }
        #endregion
    }
}