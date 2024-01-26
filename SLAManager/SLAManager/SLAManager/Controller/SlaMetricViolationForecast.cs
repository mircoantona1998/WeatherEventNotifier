using SLAManagerdata.ViewModels;
using Microsoft.AspNetCore.Mvc;
using SLAManager.Utils;
using Microsoft.AspNetCore.Authorization;
using SLAManagerdata.Models;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using SLAManager.Kafka;

namespace ExposeAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SlaMetricViolationForecastController : ControllerBase
    {
        private readonly SlaMetricViolationForecastRepository slaMetricViolationForecastRepo;
        private readonly IConfiguration _configuration;
        public SlaMetricViolationForecastController(IConfiguration configuration)
        {
            this._configuration = configuration;
            slaMetricViolationForecastRepo = new SlaMetricViolationForecastRepository(Environment.GetEnvironmentVariable("ConnectionStrings") ?? configuration.GetConnectionString("SLAManagerdata"));
        }
        #region GET
        [HttpGet]
        [Route("Get")]
        [Authorize]
        public async Task<ActionResult> Get(int? minutes,int? IdSla)
        {
            Logger log = new();
            log.LogAction("SlaMetricViolationForecastController  Get");
            string probability=null;
            if (User.Identity.IsAuthenticated)
            {
                var idUserClaim = User.FindFirst("Id");
               // int partition = Convert.ToInt32(User.FindFirst("Partition").Value);
                if (idUserClaim != null)
                {
                    var dto = new
                    {
                       Minutes = minutes,
                       IdSla = IdSla
                    };
                    var result = await Kafka.producer.ProduceRequest<string>(dto, MessageType.Request, MessageTag.GetForecast, SLAManager.Configurations.config.configuration["topic_to_forecast"], 0);
                    probability = await Kafka.consumer.ConsumeResponse<string>((int)result.Offset);
                }
            }
            else
            {
                return Problem(null, null, 401);
            }
            return probability != null ? Ok(probability) : Problem(null, null, 500);
        }
        #endregion

      
    }
}