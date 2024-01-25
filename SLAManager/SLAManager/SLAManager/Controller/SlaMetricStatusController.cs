using SLAManagerdata.ViewModels;
using Microsoft.AspNetCore.Mvc;
using SLAManager.Utils;
using Microsoft.AspNetCore.Authorization;
using SLAManagerdata.Models;

namespace ExposeAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SlaMetricStatusController : ControllerBase
    {
        private readonly SlaMetricStatusRepository slaMetricStatusRepo;
        private readonly IConfiguration _configuration;
        public SlaMetricStatusController(IConfiguration configuration)
        {
            this._configuration = configuration;
            slaMetricStatusRepo = new SlaMetricStatusRepository(Environment.GetEnvironmentVariable("ConnectionStrings") ?? configuration.GetConnectionString("SLAManagerdata"));
        }
        #region GET
        [HttpGet]
        [Route("Get")]
        [Authorize]
        public async Task<ActionResult> Get()
        {
            Logger log = new();
            log.LogAction("SlaMetricStatusController  Get");
            List<SlaMetricStatusView> SlaMetricStatuss = new List<SlaMetricStatusView>();
            if (User.Identity.IsAuthenticated)
            {
                var idUserClaim = User.FindFirst("Id");
               // int partition = Convert.ToInt32(User.FindFirst("Partition").Value);
                if (idUserClaim != null && int.TryParse(idUserClaim.Value, out int idUser))
                {
                    SlaMetricStatuss = await slaMetricStatusRepo.Get();
                }
            }
            else
            {
                return Problem(null, null, 401);
            }
            return SlaMetricStatuss != null ? Ok(SlaMetricStatuss) : Problem(null, null, 500);
        }
        #endregion

    }
}