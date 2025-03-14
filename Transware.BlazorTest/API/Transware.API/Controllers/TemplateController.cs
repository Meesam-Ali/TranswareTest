using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Transware.API.Model;

namespace Transware.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TemplateController : ControllerBase
    {
        private readonly ILogger<TemplateController> _logger;
        private List<Template?> _templates =
        [
            new()
            {
                Name = "Template1",
                Id = 1,
                Folder = new Folder() { Id = 2, Name = "First", ParentFolder = new Folder() { Id = 1, Name = "Root" } },
                Attributes = new Dictionary<string, DataType>(){{"Name",DataType.Text}, { "Reason", DataType.Text } }
            },
            new()
            {
                Name = "Template2",
                Id = 2,
                Folder = new Folder() { Id = 3, Name = "Second", ParentFolder = new Folder() { Id = 1, Name = "Root" } },
                Attributes = new Dictionary<string, DataType>(){{"Name",DataType.Text}, { "Reason", DataType.Text } }
            }
        ];

        public TemplateController(ILogger<TemplateController> logger)
        {
            _logger = logger;
        }

        [HttpGet("get-templates")]
        public IEnumerable<Dictionary<string, object>> Get()
        {
            List<Dictionary<string, object>> retVal = new();
            foreach (var template in _templates)
            {
                var dict = new Dictionary<string, object>() { { "Id", template.Id }, { "Name", template.Name } };
                retVal.Add(dict);
            }
            return retVal;
        }

        [HttpGet("get-template/{id}")]
        public Template Get(int id)
        {
            return _templates.FirstOrDefault(x => x.Id == id) ?? null;
        }

        [HttpPut("execute-template/{id}")]
        public DateTime Execute(int id, [FromBody] Dictionary<string, KeyValuePair<DataType, string>> Attributes)
        {
            //TODO: Fetch the template and insert Execution

            return DateTime.UtcNow;
        }
    }
}
