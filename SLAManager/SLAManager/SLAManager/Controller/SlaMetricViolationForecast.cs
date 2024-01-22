using SLAManagerdata.ViewModels;
using Microsoft.AspNetCore.Mvc;
using SLAManager.Utils;
using Microsoft.AspNetCore.Authorization;
using SLAManagerdata.Models;

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
        public async Task<ActionResult> Get()
        {
            Logger log = new();
            log.LogAction("SlaMetricViolationForecastController  Get");
            List<SlaMetricViolationForecastView> SlaMetricViolationForecasts = new List<SlaMetricViolationForecastView>();
            if (User.Identity.IsAuthenticated)
            {
                var idUserClaim = User.FindFirst("Id");
                int partition = Convert.ToInt32(User.FindFirst("Partition").Value);
                if (idUserClaim != null && int.TryParse(idUserClaim.Value, out int idUser))
                {
                    SlaMetricViolationForecasts = await slaMetricViolationForecastRepo.Get();
                }
            }
            else
            {
                return Problem(null, null, 401);
            }
            return SlaMetricViolationForecasts != null ? Ok(SlaMetricViolationForecasts) : Problem(null, null, 500);
        }
        #endregion

      
    }
}