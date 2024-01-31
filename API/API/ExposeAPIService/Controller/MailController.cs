using Userdata.ViewModels;
using Microsoft.AspNetCore.Mvc;
using ExposeAPI.Utils;
using ExposeAPI.Model;
using Microsoft.AspNetCore.Authorization;

namespace ExposeAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        #region GET
        [HttpGet]
        [Route("Get")]
        [Authorize]
        public async Task<ActionResult> Get()
        {
            Logger log = new();
            log.LogAction("MailController  Get");
            List<MailSent> messages = null;
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
                    var result = await Kafka.Kafka.producer.ProduceRequest<string>(dto, MessageType.Request, MessageTag.GetMailSent, cluster, ExposeAPI.Configurations.config.configuration["topic_to_mail"],partition);
                    messages = await Kafka.Kafka.consumer.ConsumeResponse<List<MailSent>>((int)result.Offset, cluster);
                }
            }
            else
            {
                return Problem(null, null, 401);
            }
            return messages != null ? Ok(messages) : Problem(null, null, 500);
        }
        #endregion

    }
}