using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Transware.Entities
{
    public class Folder : BaseEntity
    {
        [Column(name: "Instance-Name")]
        public string Name { get; set; }

    }
}
