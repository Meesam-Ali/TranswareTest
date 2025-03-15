using System.ComponentModel.DataAnnotations;

namespace Transware.API.Model
{
    public class Template
    {
        public int Id { get; set; }
        public Folder Folder { get; set; }
        public string Name { get; set; }
        public int ExecutionTime { get; set; }
        public Dictionary<string, DataType> Attributes { get; set; }
    }
}
