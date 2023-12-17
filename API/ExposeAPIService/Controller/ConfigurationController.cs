using Userdata.ViewModels;
using Microsoft.AspNetCore.Mvc;
using ExposeAPI.Utils;
using ExposeAPI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

namespace ExposeAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        #region GET
        [HttpGet]
        [Route("Get")]
        [Authorize]
        public async Task<ActionResult> Get()
        {
            List<ConfigurationUser> configurations = null;
            if (User.Identity.IsAuthenticated)
            {
                var idUserClaim = User.FindFirst("Id");
                if (idUserClaim != null && int.TryParse(idUserClaim.Value, out int idUser))
                {
                    var dto = new
                    {
                        IdUser = idUser
                    };
                    int offset = await Kafka.Kafka.producer.ProduceRequest<string>(dto, MessageType.Request, MessageTag.GetConfiguration);
                     configurations = await Kafka.Kafka.consumer.ConsumeResponse<List<ConfigurationUser>>(offset);
                }
            }
            return configurations != null ? Ok(configurations) : Problem(null, null, 401);
        }
        #endregion

        #region POST
        [Authorize]
        [HttpPost]
        [Route("Add")]
        public async Task<ActionResult> Create(ConfigurationCreateDTO newItemDTO)
        {
            string res=null;
            if (User.Identity.IsAuthenticated)
            {
                var idUserClaim = User.FindFirst("Id");
                if (idUserClaim != null && int.TryParse(idUserClaim.Value, out int idUser))
                {
                    var kafkaRequest = ConfigurationCreateRequestKafka.ConvertConfigurationCreateToRequestKafka(newItemDTO, idUser);
                    int offset=await Kafka.Kafka.producer.ProduceRequest<string>(kafkaRequest,MessageType.Request,MessageTag.AddConfiguration);
                    res=await Kafka.Kafka.consumer.ConsumeResponse<string>(offset);
                }
            }
            return res != null? Ok(res) : Problem(null, null, 401);
        }
        #endregion

        #region PATCH
        [HttpPatch]
        [Route("Patch")]
        [Authorize]
        public async Task<ActionResult> Patch(ConfigurationPatchDTO newItemDTO)
        {
            string res = null;
            if (User.Identity.IsAuthenticated)
            {
                var idUserClaim = User.FindFirst("Id");
                if (idUserClaim != null && int.TryParse(idUserClaim.Value, out int idUser))
                {
                    var kafkaRequest = ConfigurationPatchRequestKafka.ConvertConfigurationPatchToRequestKafka(newItemDTO, idUser);
                    int offset = await Kafka.Kafka.producer.ProduceRequest<string>(kafkaRequest, MessageType.Request, MessageTag.PatchConfiguration);
                    res = await Kafka.Kafka.consumer.ConsumeResponse<string>(offset);
                }
            }
            return res !=null? Ok(res) : Problem(null, null, 401);
        }
        #endregion

        #region DELETE
        [HttpDelete]
        [Route("Delete")]
        [Authorize]
        public async Task<ActionResult> Delete( int? IdConfiguration)
        {
            string res = null;
            if (User.Identity.IsAuthenticated)
            {
                var idUserClaim = User.FindFirst("Id");
                if (idUserClaim != null && int.TryParse(idUserClaim.Value, out int idUser))
                {
                    var deleteItemDTO = new
                    {
                        IdUser = idUser,
                        IdConfiguration = IdConfiguration,
                    };
                    int offset = await Kafka.Kafka.producer.ProduceRequest<string>(deleteItemDTO, MessageType.Request, MessageTag.DeleteConfiguration);
                    res = await Kafka.Kafka.consumer.ConsumeResponse<string>(offset);
                }
            }
            return res !=null ? Ok(res) : Problem(null, null, 401);
        }
        #endregion
    }
}