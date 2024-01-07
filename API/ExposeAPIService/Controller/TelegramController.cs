using Userdata.ViewModels;
using Microsoft.AspNetCore.Mvc;
using ExposeAPI.Utils;
using ExposeAPI.Model;
using Microsoft.AspNetCore.Authorization;

namespace ExposeAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TelegramController : ControllerBase
    {
        #region GET
        [HttpGet]
        [Route("Get")]
        [Authorize]
        public async Task<ActionResult> Get()
        {
            Logger log = new();
            log.LogAction("TelegramController  Get");
            List<TelegramSent> messages = null;
            if (User.Identity.IsAuthenticated)
            {
                var idUserClaim = User.FindFirst("Id");
                if (idUserClaim != null && int.TryParse(idUserClaim.Value, out int idUser))
                {
                    var dto = new
                    {
                        IdUser = idUser
                    };
                    var result = await Kafka.Kafka.producer.ProduceRequest<string>(dto, MessageType.Request, MessageTag.GetTelegramSent, ExposeAPI.Configurations.config.configuration["topic_to_telegram"]);
                    messages = await Kafka.Kafka.consumer.ConsumeResponse<List<TelegramSent>>((int)result.Offset);
                }
            }
            return messages != null ? Ok(messages) : Problem(null, null, 401);
        }
        #endregion

      
    }
}