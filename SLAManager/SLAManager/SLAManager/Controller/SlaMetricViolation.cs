using SLAManagerdata.ViewModels;
using Microsoft.AspNetCore.Mvc;
using SLAManager.Utils;
using Microsoft.AspNetCore.Authorization;
using SLAManagerdata.Models;
using System.ComponentModel.DataAnnotations;

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
        public async Task<ActionResult> Get([RegularExpression("^(1|3|6)$", ErrorMessage = "The value must be 1, 3, or 6. White space for all")] int? hours)
        {
            Logger log = new();
            log.LogAction("SlaMetricViolationController  Get");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            List<SlaMetricViolationView> SlaMetricViolations = new List<SlaMetricViolationView>();
            if (User.Identity.IsAuthenticated)
            {
                var idUserClaim = User.FindFirst("Id");
               // int partition = Convert.ToInt32(User.FindFirst("Partition").Value);
                if (idUserClaim != null && int.TryParse(idUserClaim.Value, out int idUser))
                {
                    SlaMetricViolations = await slaMetricViolationRepo.Get(hours);
                }
            }
            else
            {
                return Problem(null, null, 401);
            }
            return SlaMetricViolations != null ? Ok(SlaMetricViolations) : Problem(null, null, 500);
        }
        [HttpGet]
        [Route("GetCount")]
        [Authorize]
        public async Task<ActionResult> GetCount([RegularExpression("^(1|3|6)$", ErrorMessage = "The value must be 1, 3, or 6. White space for all")] int? hours)
        {
            Logger log = new();
            log.LogAction("SlaMetricViolationController  Get");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int? SlaMetricViolationsCount=null;
            if (User.Identity.IsAuthenticated)
            {
                var idUserClaim = User.FindFirst("Id");
                // int partition = Convert.ToInt32(User.FindFirst("Partition").Value);
                if (idUserClaim != null && int.TryParse(idUserClaim.Value, out int idUser))
                {
                    SlaMetricViolationsCount = await slaMetricViolationRepo.GetCount(hours);
                }
            }
            else
            {
                return Problem(null, null, 401);
            }
            return SlaMetricViolationsCount != null ? Ok(SlaMetricViolationsCount) : Problem(null, null, 500);
        }
        #endregion

    }
}