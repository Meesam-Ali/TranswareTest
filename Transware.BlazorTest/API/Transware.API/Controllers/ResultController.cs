using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Transware.API.Model;
using Transware.API.Utilities;
using Transware.DB;

namespace Transware.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResultController : ControllerBase
    {
        private readonly ILogger<ResultController> _logger;
        public ResultController(ILogger<ResultController> logger)
        {
            _logger = logger;
        }

        [HttpGet("get-results")]
        public IEnumerable<Dictionary<string, object>> Get()
        {
            List<Dictionary<string, object>> retVal = new();
            foreach (var result in DatabaseContext.GetInstance().Results)
            {
                var dict = new Dictionary<string, object>() { { "Id", result.Id }, { "Name", result.Name } };
                retVal.Add(dict);
            }
            return retVal;
        }

        [HttpGet("get-result/{id}")]
        public IActionResult Get(int id)
        {
            var result = Mapper.GetResultModel(DatabaseContext.GetInstance().Results.FirstOrDefault(x => x.Id == id));
            return result != null ? StatusCode(StatusCodes.Status200OK, result) : StatusCode(StatusCodes.Status404NotFound);
        }

        [HttpDelete("delete-result/{id}")]
        public IActionResult Delete(int id)
        {
            var result = DatabaseContext.GetInstance().Results.FirstOrDefault(x => x.Id == id) ?? null;
            if (result == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            else
            {
                DatabaseContext.GetInstance().Results.Remove(result);
                return StatusCode(StatusCodes.Status200OK, result.Id);
            }

        }

        [HttpGet("reset-session-state")]
        public IActionResult Reset()
        {
            DatabaseContext.Reset();
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
