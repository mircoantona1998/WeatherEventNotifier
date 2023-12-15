using Userdata.ViewModels;
using Microsoft.AspNetCore.Mvc;
using ExposeAPI.Utils;
using ExposeAPI.Model;
using Microsoft.AspNetCore.Authorization;

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
        public async Task<ActionResult> Get(int? IdUser)
        {
            var dto = new
            {
                IdUser = IdUser 
            };
            int offset = await Kafka.Kafka.producer.ProduceRequest<string>(dto, MessageType.Request, MessageTag.GetConfiguration);
            var configurations = await Kafka.Kafka.consumer.ConsumeResponse<List<ConfigurationUser>>(offset);
            return configurations != null ? Ok(configurations) : Problem(null, null, 401);
        }
        #endregion

        #region POST
        [Authorize]
        [HttpPost]
        [Route("Add")]
        public async Task<ActionResult> Create(ConfigurationCreateDTO newItemDTO)
        {
            int offset=await Kafka.Kafka.producer.ProduceRequest<string>(newItemDTO,MessageType.Request,MessageTag.AddConfiguration);
            bool res=await Kafka.Kafka.consumer.ConsumeResponse<bool>(offset);
            return res ? Ok(true) : Problem(null, null, 401);
        }
        #endregion

        #region PATCH
        [HttpPatch]
        [Route("Patch")]
        [Authorize]
        public async Task<ActionResult> Patch(ConfigurationPatchDTO newItemDTO)
        {
            int offset = await Kafka.Kafka.producer.ProduceRequest<string>(newItemDTO, MessageType.Request, MessageTag.PatchConfiguration);
            bool res = await Kafka.Kafka.consumer.ConsumeResponse<bool>(offset);
            return res ? Ok(true) : Problem(null, null, 401);
        }
        #endregion

        #region DELETE
        [HttpDelete]
        [Route("Delete")]
        [Authorize]
        public async Task<ActionResult> Delete(int? IdUser, int? IdConfiguration)
        {
            var deleteItemDTO = new
            {
                IdUser = IdUser,
                IdConfiguration=IdConfiguration,
            };
            int offset = await Kafka.Kafka.producer.ProduceRequest<string>(deleteItemDTO, MessageType.Request, MessageTag.DeleteConfiguration);
            bool isDeleted = await Kafka.Kafka.consumer.ConsumeResponse<bool>(offset);
            return isDeleted ? Ok(isDeleted) : Problem(null, null,  401);
        }
        #endregion
    }
}