using Userdata.ViewModels;
using Microsoft.AspNetCore.Mvc;
using ExposeAPI.Utils;
using ExposeAPI.Model;
using Microsoft.AspNetCore.Authorization;
using Confluent.Kafka;

namespace ExposeAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NotifierController : ControllerBase
    {
        #region GET
        [HttpGet]
        [Route("Get")]
        [Authorize]
        public async Task<ActionResult> Get()
        {
            Logger log = new();
            log.LogAction("NotifierController  Get");
            List<Notifier> configurations = null;
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
                    var result = await Kafka.Kafka.producer.ProduceRequest<string>(dto, MessageType.Request, MessageTag.GetNotify, ExposeAPI.Configurations.config.configuration["topic_to_notifier"], partition);
                    configurations = await Kafka.Kafka.consumer.ConsumeResponse<List<Notifier>>((int)result.Offset);
                }
            }
            else
            {
                return Problem(null, null, 401);
            }
            return configurations != null ? Ok(configurations) : Problem(null, null, 500);
        }
        #endregion

    }
}