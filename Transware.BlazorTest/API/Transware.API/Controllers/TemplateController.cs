using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Transware.API.Model;
using Transware.API.Utilities;
using Transware.DB;
using Transware.Entities;

namespace Transware.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TemplateController : ControllerBase
    {
        private readonly ILogger<TemplateController> _logger;
        public TemplateController(ILogger<TemplateController> logger)
        {
            _logger = logger;
        }

        [HttpGet("get-templates")]
        public IEnumerable<Dictionary<string, object>> Get()
        {
            List<Dictionary<string, object>> retVal = new();
            foreach (var template in DatabaseContext.GetInstance().Templates)
            {
                var dict = new Dictionary<string, object>() { { "Id", template.Id }, { "Name", template.Name } };
                retVal.Add(dict);
            }
            return retVal;
        }

        [HttpGet("get-template/{id}")]
        public IActionResult Get(int id)
        {
            var template = Mapper.GetTemplateModel(DatabaseContext.GetInstance().Templates.FirstOrDefault(x => x.Id == id));
            return template != null ? StatusCode(StatusCodes.Status200OK, template) : StatusCode(StatusCodes.Status404NotFound);
        }

        [HttpPut("execute-template/{id}")]
        public DateTime Execute(int id, [FromBody] Dictionary<string, KeyValuePair<Model.DataType, string>> Attributes)
        {
            var template = Mapper.GetTemplateModel(DatabaseContext.GetInstance().Templates.FirstOrDefault(x => x.Id == id));
            Model.Execution execution = new Model.Execution();
            execution.Id = DatabaseContext.GetInstance().Executions.Count > 0 ? DatabaseContext.GetInstance().Executions.Max(x => x.Id) + 1 : 1;
            execution.Status = Model.Status.Running;
            execution.StartTime = DateTime.UtcNow;
            execution.EndTime = DateTime.UtcNow.AddMinutes(template.ExecutionTime);
            execution.Attributes = Attributes;
            execution.Folder = template.Folder;
            execution.Name = "Execution-" + template.Name + "-" + DateTime.UtcNow.ToString();
            execution.Template = template;
            DatabaseContext.GetInstance().Executions.Add(Mapper.GetExecutionEntities(execution));
            return execution.StartTime;
        }
    }
}
