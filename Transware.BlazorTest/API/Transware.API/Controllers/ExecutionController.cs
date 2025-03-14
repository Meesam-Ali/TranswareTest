using System.Net;
using Microsoft.AspNetCore.Mvc;
using Transware.API.Model;

namespace Transware.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExecutionController : ControllerBase
    {
        private readonly ILogger<ExecutionController> _logger;
        private List<Execution?> _executions =
        [
            new()
            {
                Name = "Execution Of Template 1",
                Id = 1,
                Status = Status.Running,
                StartTime = DateTime.UtcNow,
                TemplateId = 1
            },
            new()
            {
                Name = "Execution Of Template 2",
                Id = 2,
                Status = Status.Finished,
                StartTime = DateTime.UtcNow.AddDays(-1),
                EndTime = DateTime.UtcNow.AddHours(-12),
                TemplateId = 2
            }
        ];

        public ExecutionController(ILogger<ExecutionController> logger)
        {
            _logger = logger;
        }

        [HttpGet("get-executions")]
        public IEnumerable<Dictionary<string, object>> Get()
        {
            List<Dictionary<string, object>> retVal = new();
            foreach (var execution in _executions)
            {
                var dict = new Dictionary<string, object>() { { "Id", execution.Id }, { "Name", execution.Name } };
                retVal.Add(dict);
            }
            return retVal;
        }

        [HttpGet("get-execution/{id}")]
        public Execution Get(int id)
        {
            return _executions.FirstOrDefault(x => x.Id == id) ?? null;
        }

        [HttpGet("abort-execution/{id}")]
        public IActionResult Abort(int id)
        {
            var execution = _executions.FirstOrDefault(x => x.Id == id) ?? null;
            if (execution.Status != Status.Running)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            else
            {
                execution.Status = Status.Aborted;
                //TODO : SAVE
                return StatusCode(StatusCodes.Status200OK, execution.Id);
            }


        }
    }
}
