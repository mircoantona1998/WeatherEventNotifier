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
            var result = await Kafka.Kafka.producer.ProduceRequest<string>("", MessageType.Request, MessageTag.GetMetric, ExposeAPI.Configurations.config.configuration["topic_to_configuration"]);
            var Metrics = await Kafka.Kafka.consumer.ConsumeResponse<List<Metric>>((int)result.Offset);
            return Metrics != null ? Ok(Metrics) : Problem(null, null, 401);
        }
        #endregion

        #region POST
        [Authorize]
        [HttpPost]
        [Route("Add")]
        public async Task<ActionResult> Create(MetricCreateDTO newItemDTO)
        {
            var result=await Kafka.Kafka.producer.ProduceRequest<string>(newItemDTO,MessageType.Request,MessageTag.AddMetric, ExposeAPI.Configurations.config.configuration["topic_to_configuration"]);
            string res=await Kafka.Kafka.consumer.ConsumeResponse<string>((int)result.Offset);
            return res!=null ? Ok(res) : Problem(null, null, 401);
        }
        #endregion

        #region PATCH
        [HttpPatch]
        [Route("Patch")]
        [Authorize]
        public async Task<ActionResult> Patch(MetricPatchDTO newItemDTO)
        {
            var result = await Kafka.Kafka.producer.ProduceRequest<string>(newItemDTO, MessageType.Request, MessageTag.PatchMetric, ExposeAPI.Configurations.config.configuration["topic_to_configuration"]);
            string res = await Kafka.Kafka.consumer.ConsumeResponse<string>((int)result.Offset);
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
            var result = await Kafka.Kafka.producer.ProduceRequest<string>(deleteItemDTO, MessageType.Request, MessageTag.DeleteMetric, ExposeAPI.Configurations.config.configuration["topic_to_configuration"]);
            string isDeleted = await Kafka.Kafka.consumer.ConsumeResponse<string>((int)result.Offset);
            return isDeleted!=null ? Ok(isDeleted) : Problem(null, null,  401);
        }
        #endregion
    }
}