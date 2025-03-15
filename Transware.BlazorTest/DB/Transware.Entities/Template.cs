using System.ComponentModel.DataAnnotations.Schema;

namespace Transware.Entities
{
    public class Template : BaseEntity
    {
        [Column(name: "Instance-Name")]
        public string Name { get; set; }

        [Column(name: "Execution-Time")]
        public int ExecutionTime { get; set; }

        public Dictionary<string, DataType> Attributes { get; set; }
    }
}
