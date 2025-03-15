using System.ComponentModel.DataAnnotations;

namespace Transware.API.Model
{
    public class Result
    {
        public int Id { get; set; }
        public Folder Folder { get; set; }
        public Execution Execution { get; set; }
        public string Name { get; set; }
        public Dictionary<string, KeyValuePair<DataType, string>> Attributes { get; set; }
        public double TimeRequired { get; set; }
    }
}
