using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Transware.Entities
{
    public class Template : BaseEntity
    {
        [Column(name: "Instance-Name")]
        public string Name { get; set; }

        [Column(name: "Execution-Time")]
        public DateTime ExecutionTime { get; set; }

        public Dictionary<string, DataType> Attributes { get; set; }
    }
}
