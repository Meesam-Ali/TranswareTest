using System.Net;
using Microsoft.AspNetCore.Mvc;
using Transware.API.Model;
using Transware.API.Utilities;
using Transware.DB;
using Transware.Entities;

namespace Transware.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExecutionController : ControllerBase
    {
        private readonly ILogger<ExecutionController> _logger;
        public ExecutionController(ILogger<ExecutionController> logger)
        {
            _logger = logger;
        }

        [HttpGet("get-executions")]
        public IEnumerable<Dictionary<string, object>> Get()
        {
            List<Dictionary<string, object>> retVal = new();
            foreach (var execution in DatabaseContext.GetInstance().Executions)
            {
                var dict = new Dictionary<string, object>() { { "Id", execution.Id }, { "Name", execution.Name }, { "Status", execution.Status } };
                retVal.Add(dict);
            }
            return retVal;
        }

        [HttpGet("get-execution/{id}")]
        public IActionResult Get(int id)
        {
            var exec = Mapper.GetExecutionModel(DatabaseContext.GetInstance().Executions.FirstOrDefault(x => x.Id == id));
            return exec != null ? StatusCode(StatusCodes.Status200OK, exec) : StatusCode(StatusCodes.Status404NotFound);
        }

        [HttpGet("abort-execution/{id}")]
        public IActionResult Abort(int id)
        {
            var execution = DatabaseContext.GetInstance().Executions.FirstOrDefault(x => x.Id == id);
            if (execution.Status != Entities.Status.Running)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            else
            {
                execution.Status = Entities.Status.Aborted;
                var result = new Entities.Result();
                result.Attributes = execution.Attributes;
                result.ExecutionId = execution.Id;
                result.FolderId = execution.FolderId;
                result.Name = execution.Name + "-result";
                result.TimeRequired = (DateTime.UtcNow - execution.StartTime).TotalMinutes;
                result.Id = DatabaseContext.GetInstance().Results.Count > 0 ? DatabaseContext.GetInstance().Results.Max(x => x.Id) + 1 : 1;
                DatabaseContext.GetInstance().Results.Add(result);

                return StatusCode(StatusCodes.Status200OK, execution.Id);
            }


        }
    }
}
