using Userdata.ViewModels;
using Microsoft.AspNetCore.Mvc;
using ExposeAPI.Utils;
using ExposeAPI.Model;
using Microsoft.AspNetCore.Authorization;

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
        public async Task<ActionResult> Get()
        {
            Logger log = new();
            log.LogAction("UserMailController  Get");
            List<UserMail> usermail = null;
            if (User.Identity.IsAuthenticated)
            {
                var idUserClaim = User.FindFirst("Id");
                if (idUserClaim != null && int.TryParse(idUserClaim.Value, out int idUser))
                {
                    var dto = new
                    {
                        IdUser = idUser
                    };
                    var result = await Kafka.Kafka.producer.ProduceRequest<string>(dto, MessageType.Request, MessageTag.GetUserMail, ExposeAPI.Configurations.config.configuration["topic_to_mail"]);
                    usermail = await Kafka.Kafka.consumer.ConsumeResponse<List<UserMail>>((int)result.Offset);
                }
            }
            return usermail != null ? Ok(usermail) : Problem(null, null, 401);
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
                if (idUserClaim != null && int.TryParse(idUserClaim.Value, out int idUser))
                {
                    var kafkaRequest = MailCreateRequestKafka.ConvertMailCreateToRequestKafka(newItemDTO, idUser);
                    var result = await Kafka.Kafka.producer.ProduceRequest<string>(kafkaRequest, MessageType.Request, MessageTag.AddUserMail, ExposeAPI.Configurations.config.configuration["topic_to_mail"]);
                    res = await Kafka.Kafka.consumer.ConsumeResponse<string>((int)result.Offset);
                }
            }
            return res!=null ? Ok(res) : Problem(null, null, 401);
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
                if (idUserClaim != null && int.TryParse(idUserClaim.Value, out int idUser))
                {
                    var kafkaRequest = MailPatchRequestKafka.ConvertMailPatchToRequestKafka(newItemDTO, idUser);
                    var result = await Kafka.Kafka.producer.ProduceRequest<string>(kafkaRequest, MessageType.Request, MessageTag.PatchUserMail, ExposeAPI.Configurations.config.configuration["topic_to_mail"]);
                    res = await Kafka.Kafka.consumer.ConsumeResponse<string>((int)result.Offset);
                }
            }
            return res != null? Ok(res) : Problem(null, null, 401);
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
                if (idUserClaim != null && int.TryParse(idUserClaim.Value, out int idUser))
                {
                    var dto = new
                    {
                        IdUser = idUser
                    };
                    var result = await Kafka.Kafka.producer.ProduceRequest<string>(dto, MessageType.Request, MessageTag.DeleteUserMail, ExposeAPI.Configurations.config.configuration["topic_to_mail"]);
                    isDeleted = await Kafka.Kafka.consumer.ConsumeResponse<string>((int)result.Offset);
                }
            }
            return isDeleted != null ? Ok(isDeleted) : Problem(null, null,  401);
        }
        #endregion
    }
}