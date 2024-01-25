using SLAManagerdata.ViewModels;
using Microsoft.AspNetCore.Mvc;
using SLAManager.Utils;
using Microsoft.AspNetCore.Authorization;
using SLAManagerdata.Models;


namespace SLAManager.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SlaController : ControllerBase
    {
        private readonly SlaRepository slaRepo;
        private readonly IConfiguration _configuration;
        public SlaController(IConfiguration configuration)
        {
            this._configuration = configuration;
            slaRepo = new SlaRepository(Environment.GetEnvironmentVariable("ConnectionStrings") ?? configuration.GetConnectionString("SLAManagerdata"));
        }
        #region GET
        [HttpGet]
        [Route("Get")]
        [Authorize]
        public async Task<ActionResult> Get()
        {
            Logger log = new();
            log.LogAction("Sla controller  Get");
            List<SLAManagerdata.Models.SlaView> Slas = null;
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
                    Slas = await slaRepo.Get();
                }
            }
            else
            {
                return Problem(null, null, 401);
            }
            return Slas != null ? Ok(Slas) : Problem(null, null, 500);
        }
        #endregion

        #region POST
        [Authorize]
        [HttpPost]
        [Route("Add")]
        public async Task<ActionResult> Create(SlaAddDTO newItemDTO)
        {
            Logger log = new();
            log.LogAction("SlaController  Create");
            bool? res=false;
            if (User.Identity.IsAuthenticated)
            {
                var idUserClaim = User.FindFirst("Id");
               // int partition = Convert.ToInt32(User.FindFirst("Partition").Value);
                if (idUserClaim != null && int.TryParse(idUserClaim.Value, out int idUser))
                {
                    if ( newItemDTO.Symbol == ">" || newItemDTO.Symbol == ">=" || newItemDTO.Symbol == "==" || newItemDTO.Symbol == "<" || newItemDTO.Symbol == "<=")
                    {

                        res = await slaRepo.Create(newItemDTO);//, partition);
                    }else return Problem("Inserire un simbolo valido", null, 500);
                }
            }
            else
            {
                return Problem(null, null, 401);
            }
            return res != null ? Ok(res) : Problem(null, null, 500);
        }
        #endregion

        #region PATCH
        [HttpPatch]
        [Route("Patch")]
        [Authorize]
        public async Task<ActionResult> Patch(SlaPatchDTO newItemDTO)
        {
            Logger log = new();
            log.LogAction("SlaController  Patch");
            bool? res = false;
            if (User.Identity.IsAuthenticated)
            {
                var idUserClaim = User.FindFirst("Id");
                int partition = Convert.ToInt32(User.FindFirst("Partition").Value);
                if (idUserClaim != null && int.TryParse(idUserClaim.Value, out int idUser))
                {
                    if (newItemDTO.Symbol==null || newItemDTO.Symbol == ">" || newItemDTO.Symbol == ">=" || newItemDTO.Symbol == "==" || newItemDTO.Symbol == "<" || newItemDTO.Symbol == "<=")
                    {
                        res = await slaRepo.Patch(newItemDTO, partition);
                    }
                    else return Problem("Inserire un simbolo valido", null, 500);
                }
            }
            else
            {
                return Problem(null, null, 401);
            }
            return res !=null? Ok(res) : Problem(null, null, 500);
        }
        #endregion

        #region DELETE
        [HttpDelete]
        [Route("Delete")]
        [Authorize]
        public async Task<ActionResult> Delete( int? IdSla)
        {
            Logger log = new();
            log.LogAction("SlaController  Delete");
            bool res = false;
            if (User.Identity.IsAuthenticated)
            {
                var idUserClaim = User.FindFirst("Id");
                int partition = Convert.ToInt32(User.FindFirst("Partition").Value);
                if (idUserClaim != null && int.TryParse(idUserClaim.Value, out int idUser))
                {
                    res = await slaRepo.Delete(IdSla);
                }
            }
            else
            {
                return Problem(null, null, 401);
            }
            return res != null ? Ok(res) : Problem(null, null, 500);
        }
        #endregion
    }
}