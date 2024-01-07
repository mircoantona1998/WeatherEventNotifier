using Userdata.ViewModels;
using Microsoft.AspNetCore.Mvc;
using ExposeAPI.Utils;
using ExposeAPI.Model;
using Microsoft.AspNetCore.Authorization;

namespace ExposeAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SchedulerController : ControllerBase
    {
        #region GET
        [HttpGet]
        [Route("Get")]
        [Authorize]
        public async Task<ActionResult> Get()
        {
            Logger log = new();
            log.LogAction("SchedulerController  Get");
            List<Schedulation> schedulations = null;
            if (User.Identity.IsAuthenticated)
            {
                var idUserClaim = User.FindFirst("Id");
                if (idUserClaim != null && int.TryParse(idUserClaim.Value, out int idUser))
                {
                    var dto = new
                    {
                        IdUser = idUser
                    };
                    var result = await Kafka.Kafka.producer.ProduceRequest<string>(dto, MessageType.Request, MessageTag.GetSchedulation, ExposeAPI.Configurations.config.configuration["topic_to_scheduler"]);
                    schedulations = await Kafka.Kafka.consumer.ConsumeResponse<List<Schedulation>>((int)result.Offset);
                }
            }
            return schedulations != null ? Ok(schedulations) : Problem(null, null, 401);
        }
        #endregion

      
    }
}