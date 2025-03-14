using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Transware.API.Model;

namespace Transware.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResultController : ControllerBase
    {
        private readonly ILogger<ResultController> _logger;
        private List<Result?> _results =
        [
            new()
            {
                Name = "Result1",
                Id = 1,
                ExecutionId = 1,
                FolderId = 1,
                TimeRequired = 24

            },
            new()
            {
                Name = "Result2",
                Id = 2,
                ExecutionId = 2,
                FolderId = 2,
                TimeRequired = 24
            }
        ];

        public ResultController(ILogger<ResultController> logger)
        {
            _logger = logger;
        }

        [HttpGet("get-results")]
        public IEnumerable<Dictionary<string, object>> Get()
        {
            List<Dictionary<string, object>> retVal = new();
            foreach (var result in _results)
            {
                var dict = new Dictionary<string, object>() { { "Id", result.Id }, { "Name", result.Name } };
                retVal.Add(dict);
            }
            return retVal;
        }

        [HttpGet("get-result/{id}")]
        public Result Get(int id)
        {
            return _results.FirstOrDefault(x => x.Id == id) ?? null;
        }

        [HttpDelete("delete-result/{id}")]
        public IActionResult Delete(int id)
        {
            var result = _results.FirstOrDefault(x => x.Id == id) ?? null;
            if (result == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            else
            {
                //TODO : Fetch execution and remove it.
                return StatusCode(StatusCodes.Status200OK, result.Id);
            }

        }


        [HttpGet("reset-session-state")]
        public IActionResult Reset()
        {
            //TODO All session related Execution and Result data is removed.
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
