using SLAManagerdata.ViewModels;
using Microsoft.AspNetCore.Mvc;
using SLAManager.Utils;
using Microsoft.AspNetCore.Authorization;
using SLAManagerdata.Models;

namespace ExposeAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SlaMetricViolationController : ControllerBase
    {
        private readonly SlaMetricViolationRepository slaMetricViolationRepo;
        private readonly IConfiguration _configuration;
        public SlaMetricViolationController(IConfiguration configuration)
        {
            this._configuration = configuration;
            slaMetricViolationRepo = new SlaMetricViolationRepository(Environment.GetEnvironmentVariable("ConnectionStrings") ?? configuration.GetConnectionString("SLAManagerdata"));
        }
        #region GET
        [HttpGet]
        [Route("Get")]
        [Authorize]
        public async Task<ActionResult> Get()
        {
            Logger log = new();
            log.LogAction("SlaMetricViolationController  Get");
            List<SlaMetricViolation> SlaMetricViolations = new List<SlaMetricViolation>();
            if (User.Identity.IsAuthenticated)
            {
                var idUserClaim = User.FindFirst("Id");
                int partition = Convert.ToInt32(User.FindFirst("Partition").Value);
                if (idUserClaim != null && int.TryParse(idUserClaim.Value, out int idUser))
                {
                    SlaMetricViolations = await slaMetricViolationRepo.Get();
                }
            }
            else
            {
                return Problem(null, null, 401);
            }
            return SlaMetricViolations != null ? Ok(SlaMetricViolations) : Problem(null, null, 500);
        }
        #endregion

    }
}