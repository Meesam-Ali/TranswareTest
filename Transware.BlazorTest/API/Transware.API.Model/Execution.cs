using System.ComponentModel.DataAnnotations;

namespace Transware.API.Model
{
    public class Execution
    {
        public int Id { get; set; }
        public Folder Folder { get; set; }
        public Template Template { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Dictionary<string, KeyValuePair<DataType, string>> Attributes { get; set; }

    }

    public enum Status
    {
        Running,
        Finished,
        Aborted
    }
}
