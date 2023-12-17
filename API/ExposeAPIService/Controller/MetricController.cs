using Userdata.ViewModels;
using Microsoft.AspNetCore.Mvc;
using ExposeAPI.Utils;
using ExposeAPI.Model;
using Microsoft.AspNetCore.Authorization;

namespace ExposeAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MetricController : ControllerBase
    {
        #region GET
        [HttpGet]
        [Route("Get")]
        [Authorize]
        public async Task<ActionResult> Get()
        {
            int offset = await Kafka.Kafka.producer.ProduceRequest<string>("", MessageType.Request, MessageTag.GetMetric);
            var Metrics = await Kafka.Kafka.consumer.ConsumeResponse<List<Metric>>(offset);
            return Metrics != null ? Ok(Metrics) : Problem(null, null, 401);
        }
        #endregion

        #region POST
        [Authorize]
        [HttpPost]
        [Route("Add")]
        public async Task<ActionResult> Create(MetricCreateDTO newItemDTO)
        {
            int offset=await Kafka.Kafka.producer.ProduceRequest<string>(newItemDTO,MessageType.Request,MessageTag.AddMetric);
            string res=await Kafka.Kafka.consumer.ConsumeResponse<string>(offset);
            return res!=null ? Ok(res) : Problem(null, null, 401);
        }
        #endregion

        #region PATCH
        [HttpPatch]
        [Route("Patch")]
        [Authorize]
        public async Task<ActionResult> Patch(MetricPatchDTO newItemDTO)
        {
            int offset = await Kafka.Kafka.producer.ProduceRequest<string>(newItemDTO, MessageType.Request, MessageTag.PatchMetric);
            string res = await Kafka.Kafka.consumer.ConsumeResponse<string>(offset);
            return res!=null ? Ok(res) : Problem(null, null, 401);
        }
        #endregion

        #region DELETE
        [HttpDelete]
        [Route("Delete")]
        [Authorize]
        public async Task<ActionResult> Delete( int? IdMetric)
        {
            var deleteItemDTO = new
            {
                IdMetric=IdMetric,
            };
            int offset = await Kafka.Kafka.producer.ProduceRequest<string>(deleteItemDTO, MessageType.Request, MessageTag.DeleteMetric);
            string isDeleted = await Kafka.Kafka.consumer.ConsumeResponse<string>(offset);
            return isDeleted!=null ? Ok(isDeleted) : Problem(null, null,  401);
        }
        #endregion
    }
}