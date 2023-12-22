using Userdata.ViewModels;
using Microsoft.AspNetCore.Mvc;
using ExposeAPI.Utils;
using ExposeAPI.Model;
using Microsoft.AspNetCore.Authorization;

namespace ExposeAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FrequencyController : ControllerBase
    {
        #region GET
        [HttpGet]
        [Route("Get")]
        [Authorize]
        public async Task<ActionResult> Get()
        {
            var result = await Kafka.Kafka.producer.ProduceRequest<string>("", MessageType.Request, MessageTag.GetFrequency, ExposeAPI.Configurations.config.configuration["topic_to_configuration"]);
            var Frequencys = await Kafka.Kafka.consumer.ConsumeResponse<List<Frequency>>((int)result.Offset);
            return Frequencys != null ? Ok(Frequencys) : Problem(null, null, 401);
        }
        #endregion

        #region POST
        [Authorize]
        [HttpPost]
        [Route("Add")]
        public async Task<ActionResult> Create(FrequencyCreateDTO newItemDTO)
        {
            var result=await Kafka.Kafka.producer.ProduceRequest<string>(newItemDTO,MessageType.Request,MessageTag.AddFrequency, ExposeAPI.Configurations.config.configuration["topic_to_configuration"]);
            string res=await Kafka.Kafka.consumer.ConsumeResponse<string>((int)result.Offset);
            return res!=null ? Ok(res) : Problem(null, null, 401);
        }
        #endregion

        #region PATCH
        [HttpPatch]
        [Route("Patch")]
        [Authorize]
        public async Task<ActionResult> Patch(FrequencyPatchDTO newItemDTO)
        {
            var result = await Kafka.Kafka.producer.ProduceRequest<string>(newItemDTO, MessageType.Request, MessageTag.PatchFrequency, ExposeAPI.Configurations.config.configuration["topic_to_configuration"]);
            string res = await Kafka.Kafka.consumer.ConsumeResponse<string>((int)result.Offset);
            return res != null? Ok(res) : Problem(null, null, 401);
        }
        #endregion

        #region DELETE
        [HttpDelete]
        [Route("Delete")]
        [Authorize]
        public async Task<ActionResult> Delete( int? IdFrequency)
        {
            var deleteItemDTO = new
            {
                IdFrequency=IdFrequency,
            };
            var result = await Kafka.Kafka.producer.ProduceRequest<string>(deleteItemDTO, MessageType.Request, MessageTag.DeleteFrequency, ExposeAPI.Configurations.config.configuration["topic_to_configuration"]);
            string isDeleted = await Kafka.Kafka.consumer.ConsumeResponse<string>((int)result.Offset);
            return isDeleted != null ? Ok(isDeleted) : Problem(null, null,  401);
        }
        #endregion
    }
}