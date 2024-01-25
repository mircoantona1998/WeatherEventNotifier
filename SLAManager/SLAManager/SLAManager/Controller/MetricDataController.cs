using SLAManagerdata.ViewModels;
using Microsoft.AspNetCore.Mvc;
using SLAManager.Utils;
using Microsoft.AspNetCore.Authorization;
using SLAManagerdata.Models;

namespace SLAManager.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MetricDataController : ControllerBase
    {
        private readonly MetricDataRepository MetricDataRepo;
        private readonly IConfiguration _configuration;
        public MetricDataController(IConfiguration configuration)
        {
            this._configuration = configuration;
            MetricDataRepo = new MetricDataRepository(Environment.GetEnvironmentVariable("ConnectionStrings") ?? configuration.GetConnectionString("SLAManagerdata"));
        }
        #region GET
        [HttpGet]
        [Route("Get")]
        [Authorize]
        public async Task<ActionResult> Get()
        {
            Logger log = new();
            log.LogAction("MetricDataController  Get");
            List<MetricDatum> MetricDatas = new List<MetricDatum>();
            if (User.Identity.IsAuthenticated)
            {
                var idUserClaim = User.FindFirst("Id");
                int partition = Convert.ToInt32(User.FindFirst("Partition").Value);
                if (idUserClaim != null && int.TryParse(idUserClaim.Value, out int idUser))
                {
                    MetricDatas = await MetricDataRepo.Get();
                }
            }
            else
            {
                return Problem(null, null, 401);
            }
            return MetricDatas != null ? Ok(MetricDatas) : Problem(null, null, 500);
        }
        #endregion

    }
}