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
            int offset = await Kafka.Kafka.producer.ProduceRequest<string>("", MessageType.Request, MessageTag.GetFrequency);
            var Frequencys = await Kafka.Kafka.consumer.ConsumeResponse<List<Frequency>>(offset);
            return Frequencys != null ? Ok(Frequencys) : Problem(null, null, 401);
        }
        #endregion

        #region POST
        [Authorize]
        [HttpPost]
        [Route("Add")]
        public async Task<ActionResult> Create(FrequencyCreateDTO newItemDTO)
        {
            int offset=await Kafka.Kafka.producer.ProduceRequest<string>(newItemDTO,MessageType.Request,MessageTag.AddFrequency);
            string res=await Kafka.Kafka.consumer.ConsumeResponse<string>(offset);
            return res!=null ? Ok(res) : Problem(null, null, 401);
        }
        #endregion

        #region PATCH
        [HttpPatch]
        [Route("Patch")]
        [Authorize]
        public async Task<ActionResult> Patch(FrequencyPatchDTO newItemDTO)
        {
            int offset = await Kafka.Kafka.producer.ProduceRequest<string>(newItemDTO, MessageType.Request, MessageTag.PatchFrequency);
            string res = await Kafka.Kafka.consumer.ConsumeResponse<string>(offset);
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
            int offset = await Kafka.Kafka.producer.ProduceRequest<string>(deleteItemDTO, MessageType.Request, MessageTag.DeleteFrequency);
            string isDeleted = await Kafka.Kafka.consumer.ConsumeResponse<string>(offset);
            return isDeleted != null ? Ok(isDeleted) : Problem(null, null,  401);
        }
        #endregion
    }
}