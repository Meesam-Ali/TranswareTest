using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Transware.Entities
{
    public class Execution : BaseEntity
    {
        [Column("Template-ID")]
        public int TemplateId { get; set; }

        [Column(name: "Name")]
        public string Name { get; set; }

        [Column(name: "Status")]
        public Status Status { get; set; }
        
        [Column(name: "StartTime")]
        public DateTime StartTime { get; set; }
        
        [Column(name: "EndTime")]
        public DateTime EndTime { get; set; }
        
        public Dictionary<string, KeyValuePair<DataType, string>> Attributes { get; set; }
    }
}
