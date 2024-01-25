using SLAManagerdata.ViewModels;
using Microsoft.AspNetCore.Mvc;
using SLAManager.Utils;
using Microsoft.AspNetCore.Authorization;
using SLAManagerdata.Models;

namespace ExposeAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly StatusRepository statusRepo;
        private readonly IConfiguration _configuration;
        public StatusController(IConfiguration configuration)
        {
            this._configuration = configuration;
            statusRepo = new StatusRepository(Environment.GetEnvironmentVariable("ConnectionStrings") ?? configuration.GetConnectionString("SLAManagerdata"));
        }
        #region GET
        [HttpGet]
        [Route("Get")]
        [Authorize]
        public async Task<ActionResult> Get()
        {
            Logger log = new();
            log.LogAction("StatusController  Get");
            List<Status> Statuss = new List<Status>();
            if (User.Identity.IsAuthenticated)
            {
                var idUserClaim = User.FindFirst("Id");
               // int partition = Convert.ToInt32(User.FindFirst("Partition").Value);
                if (idUserClaim != null && int.TryParse(idUserClaim.Value, out int idUser))
                {
                    Statuss = await statusRepo.Get();
                }
            }
            else
            {
                return Problem(null, null, 401);
            }
            return Statuss != null ? Ok(Statuss) : Problem(null, null, 500);
        }
        #endregion

        //#region POST
        //[Authorize]
        //[HttpPost]
        //[Route("Add")]
        //public async Task<ActionResult> Create(StatusCreateDTO newItemDTO)
        //{
        //    Logger log = new();
        //    log.LogAction("StatusController  Create");
        //    string res = null;
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        var idUserClaim = User.FindFirst("Id");
        //        int partition = Convert.ToInt32(User.FindFirst("Partition").Value);
        //        if (idUserClaim != null && int.TryParse(idUserClaim.Value, out int idUser))
        //        {
        //            var result=await Kafka.Kafka.producer.ProduceRequest<string>(newItemDTO,MessageType.Request,MessageTag.AddStatus, ExposeAPI.Configurations.config.configuration["topic_to_configuration"], partition);
        //            res=await Kafka.Kafka.consumer.ConsumeResponse<string>((int)result.Offset);
        //            return res!=null ? Ok(res) : Problem(null, null, 401);
        //        }
        //    }
        //    else
        //    {
        //        return Problem(null, null, 401);
        //    }
        //    return res != null ? Ok(res) : Problem(null, null, 500);
        //}
        //#endregion

        //#region PATCH
        //[HttpPatch]
        //[Route("Patch")]
        //[Authorize]
        //public async Task<ActionResult> Patch(StatusPatchDTO newItemDTO)
        //{
        //    Logger log = new();
        //    log.LogAction("StatusController  Patch");
        //    string res = null;
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        var idUserClaim = User.FindFirst("Id");
        //        int partition = Convert.ToInt32(User.FindFirst("Partition").Value);
        //        if (idUserClaim != null && int.TryParse(idUserClaim.Value, out int idUser))
        //        {
        //            var result = await Kafka.Kafka.producer.ProduceRequest<string>(newItemDTO, MessageType.Request, MessageTag.PatchStatus, ExposeAPI.Configurations.config.configuration["topic_to_configuration"], partition);
        //            res = await Kafka.Kafka.consumer.ConsumeResponse<string>((int)result.Offset);
        //            return res!=null ? Ok(res) : Problem(null, null, 401);
        //        }
        //    }
        //    else
        //    {
        //        return Problem(null, null, 401);
        //    }
        //    return res != null ? Ok(res) : Problem(null, null, 500);
        //}
        //#endregion

        //#region DELETE
        //[HttpDelete]
        //[Route("Delete")]
        //[Authorize]
        //public async Task<ActionResult> Delete( int? IdStatus)
        //{
        //    Logger log = new();
        //    log.LogAction("StatusController  Delete");
        //    string isDeleted = null;
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        var idUserClaim = User.FindFirst("Id");
        //        int partition = Convert.ToInt32(User.FindFirst("Partition").Value);
        //        if (idUserClaim != null && int.TryParse(idUserClaim.Value, out int idUser))
        //        {
        //            var deleteItemDTO = new
        //            {
        //                IdStatus=IdStatus,
        //            };
        //            var result = await Kafka.Kafka.producer.ProduceRequest<string>(deleteItemDTO, MessageType.Request, MessageTag.DeleteStatus, ExposeAPI.Configurations.config.configuration["topic_to_configuration"], partition);
        //            isDeleted = await Kafka.Kafka.consumer.ConsumeResponse<string>((int)result.Offset);
        //            return isDeleted!=null ? Ok(isDeleted) : Problem(null, null,  401);
        //        }
        //    }
        //    else
        //    {
        //        return Problem(null, null, 401);
        //    }
        //    return isDeleted != null ? Ok(isDeleted) : Problem(null, null, 500);
        //}
        //#endregion
    }
}