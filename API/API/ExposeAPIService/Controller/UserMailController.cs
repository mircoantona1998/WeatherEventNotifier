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
    public class UserMailController : ControllerBase
    {
        #region GET
        [HttpGet]
        [Route("Get")]
        [Authorize]
        public async Task<ActionResult> Get(bool? all = null)
        {
            Logger log = new();
            log.LogAction("UserMailController  Get");
            List<UserMail> usermail = null;
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
                        All = all
                    };
                    var result = await Kafka.Kafka.producer.ProduceRequest<string>(dto, MessageType.Request, MessageTag.GetUserMail,cluster, ExposeAPI.Configurations.config.configuration["topic_to_mail"], partition);
                    usermail = await Kafka.Kafka.consumer.ConsumeResponse<List<UserMail>>((int)result.Offset,cluster, partition);
                }
            }
            else
            {
                return Problem(null, null, 401);
            }
            return usermail != null ? Ok(usermail) : Problem(null, null, 500);
        }
        #endregion

        #region POST
        [Authorize]
        [HttpPost]
        [Route("Add")]
        public async Task<ActionResult> Create(UserMailCreateDTO newItemDTO)
        {
            Logger log = new();
            log.LogAction("UserMailController  Create");
            string res = null;
            if (User.Identity.IsAuthenticated)
            {
                var idUserClaim = User.FindFirst("Id");
                int partition = Convert.ToInt32(User.FindFirst("Partition").Value);
                string cluster = User.FindFirst("Cluster").Value;
                if (idUserClaim != null && int.TryParse(idUserClaim.Value, out int idUser))
                {
                    var kafkaRequest = MailCreateRequestKafka.ConvertMailCreateToRequestKafka(newItemDTO, idUser);
                    var result = await Kafka.Kafka.producer.ProduceRequest<string>(kafkaRequest, MessageType.Request, MessageTag.AddUserMail,cluster, ExposeAPI.Configurations.config.configuration["topic_to_mail"], partition);
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
        public async Task<ActionResult> Patch(UserMailPatchDTO newItemDTO)
        {
            Logger log = new();
            log.LogAction("UserMailController  Patch");
            string res = null;
            if (User.Identity.IsAuthenticated)
            {
                var idUserClaim = User.FindFirst("Id");
                int partition = Convert.ToInt32(User.FindFirst("Partition").Value);
                string cluster = User.FindFirst("Cluster").Value;
                if (idUserClaim != null && int.TryParse(idUserClaim.Value, out int idUser))
                {
                    var kafkaRequest = MailPatchRequestKafka.ConvertMailPatchToRequestKafka(newItemDTO, idUser);
                    var result = await Kafka.Kafka.producer.ProduceRequest<string>(kafkaRequest, MessageType.Request, MessageTag.PatchUserMail,cluster, ExposeAPI.Configurations.config.configuration["topic_to_mail"], partition);
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
            log.LogAction("UserMailController  Delete");
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
                    var result = await Kafka.Kafka.producer.ProduceRequest<string>(dto, MessageType.Request, MessageTag.DeleteUserMail, cluster,ExposeAPI.Configurations.config.configuration["topic_to_mail"], partition);
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