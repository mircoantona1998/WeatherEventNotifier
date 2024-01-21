using SLAManagerdata.ViewModels;
using Microsoft.AspNetCore.Mvc;
using SLAManager.Utils;
using Microsoft.AspNetCore.Authorization;
using SLAManagerdata.Models;

namespace SLAManager.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MonitoringMetricController : ControllerBase
    {
        private readonly MonitoringMetricRepository monitoringMetricRepo;
        private readonly IConfiguration _configuration;
        public MonitoringMetricController(IConfiguration configuration)
        {
            this._configuration = configuration;
            monitoringMetricRepo = new MonitoringMetricRepository(Environment.GetEnvironmentVariable("ConnectionStrings") ?? configuration.GetConnectionString("SLAManagerdata"));
        }
        #region GET
        [HttpGet]
        [Route("Get")]
        [Authorize]
        public async Task<ActionResult> Get()
        {
            Logger log = new();
            log.LogAction("MonitoringMetricController  Get");
            List<MonitoringMetric> MonitoringMetrics = new List<MonitoringMetric>();
            if (User.Identity.IsAuthenticated)
            {
                var idUserClaim = User.FindFirst("Id");
                int partition = Convert.ToInt32(User.FindFirst("Partition").Value);
                if (idUserClaim != null && int.TryParse(idUserClaim.Value, out int idUser))
                {
                    MonitoringMetrics = await monitoringMetricRepo.Get();
                }
            }
            else
            {
                return Problem(null, null, 401);
            }
            return MonitoringMetrics != null ? Ok(MonitoringMetrics) : Problem(null, null, 500);
        }
        #endregion

    }
}