using System.ComponentModel.DataAnnotations;

namespace Transware.Entities
{
    public class Result : BaseEntity
    {
        public int ExecutionId { get; set; }
        public string Name { get; set; }
        public Dictionary<string, KeyValuePair<DataType, string>> Attributes { get; set; }
        public double TimeRequired { get; set; }
    }
}
