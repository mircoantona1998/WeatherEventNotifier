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
            Logger log = new();
            log.LogAction("ConfigurationController  Get");
            List<ConfigurationUser> configurations = null;
            if (User.Identity.IsAuthenticated)
            {
                var idUserClaim = User.FindFirst("Id");
                int partition = Convert.ToInt32(User.FindFirst("Partition").Value);
                string cluster = User.FindFirst("Cluster").Value;
                if (idUserClaim != null && int.TryParse(idUserClaim.Value, out int idUser))
                {
                    var dto = new
                    {
                        IdUser = idUser
                    };
                    var result = await Kafka.Kafka.producer.ProduceRequest<string>(dto, MessageType.Request, MessageTag.GetConfiguration,cluster, ExposeAPI.Configurations.config.configuration["topic_to_configuration"], partition);
                    configurations = await Kafka.Kafka.consumer.ConsumeResponse<List<ConfigurationUser>>((int)result.Offset,cluster);
                }
            }
            else
            {
                return Problem(null, null, 401);
            }
            return configurations != null ? Ok(configurations) : Problem(null, null, 500);
        }
        #endregion

        #region POST
        [Authorize]
        [HttpPost]
        [Route("Add")]
        public async Task<ActionResult> Create(ConfigurationCreateDTO newItemDTO)
        {
            Logger log = new();
            log.LogAction("ConfigurationController  Create");
            string res=null;
            if (User.Identity.IsAuthenticated)
            {
                var idUserClaim = User.FindFirst("Id");
                int partition = Convert.ToInt32(User.FindFirst("Partition").Value);
                string cluster = User.FindFirst("Cluster").Value;
                if (idUserClaim != null && int.TryParse(idUserClaim.Value, out int idUser))
                {
                    if (newItemDTO.Symbol == ">" || newItemDTO.Symbol == ">=" || newItemDTO.Symbol == "==" || newItemDTO.Symbol == "<" || newItemDTO.Symbol == "<=")
                    {
                        var kafkaRequest = ConfigurationCreateRequestKafka.ConvertConfigurationCreateToRequestKafka(newItemDTO, idUser);
                        var result = await Kafka.Kafka.producer.ProduceRequest<string>(kafkaRequest, MessageType.Request, MessageTag.AddConfiguration,cluster, ExposeAPI.Configurations.config.configuration["topic_to_configuration"], partition);
                        res = await Kafka.Kafka.consumer.ConsumeResponse<string>((int)result.Offset, cluster);
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
        public async Task<ActionResult> Patch(ConfigurationPatchDTO newItemDTO)
        {
            Logger log = new();
            log.LogAction("ConfigurationController  Patch");
            string res = null;
            if (User.Identity.IsAuthenticated)
            {
                var idUserClaim = User.FindFirst("Id");
                int partition = Convert.ToInt32(User.FindFirst("Partition").Value);
                string cluster = User.FindFirst("Cluster").Value;
                if (idUserClaim != null && int.TryParse(idUserClaim.Value, out int idUser))
                {
                    if (newItemDTO.Symbol==null || newItemDTO.Symbol == ">" || newItemDTO.Symbol == ">=" || newItemDTO.Symbol == "==" || newItemDTO.Symbol == "<" || newItemDTO.Symbol == "<=")
                    {
                        var kafkaRequest = ConfigurationPatchRequestKafka.ConvertConfigurationPatchToRequestKafka(newItemDTO, idUser);
                        var result = await Kafka.Kafka.producer.ProduceRequest<string>(kafkaRequest, MessageType.Request, MessageTag.PatchConfiguration,cluster, ExposeAPI.Configurations.config.configuration["topic_to_configuration"],partition);
                        res = await Kafka.Kafka.consumer.ConsumeResponse<string>((int)result.Offset, cluster);
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
        public async Task<ActionResult> Delete( int? IdConfiguration)
        {
            Logger log = new();
            log.LogAction("ConfigurationController  Delete");
            string res = null;
            if (User.Identity.IsAuthenticated)
            {
                var idUserClaim = User.FindFirst("Id");
                int partition = Convert.ToInt32(User.FindFirst("Partition").Value);
                string cluster = User.FindFirst("Cluster").Value;
                if (idUserClaim != null && int.TryParse(idUserClaim.Value, out int idUser))
                {
                    var deleteItemDTO = new
                    {
                        IdUser = idUser,
                        IdConfiguration = IdConfiguration,
                    };
                    var result = await Kafka.Kafka.producer.ProduceRequest<string>(deleteItemDTO, MessageType.Request, MessageTag.DeleteConfiguration,cluster, ExposeAPI.Configurations.config.configuration["topic_to_configuration"], partition);
                    res = await Kafka.Kafka.consumer.ConsumeResponse<string>((int)result.Offset, cluster);
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