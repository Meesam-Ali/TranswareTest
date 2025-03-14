using System.ComponentModel.DataAnnotations;

namespace Transware.API.Model
{
    public class Result
    {
        public int Id { get; set; }
        public int FolderId { get; set; }
        public int ExecutionId { get; set; }
        public string Name { get; set; }
        public Dictionary<string, KeyValuePair<DataType, string>> Attributes { get; set; }
        public double TimeRequired { get; set; }
    }
}
