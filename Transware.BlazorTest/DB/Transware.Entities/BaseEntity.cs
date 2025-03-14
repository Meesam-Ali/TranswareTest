using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Transware.Entities
{
    public class BaseEntity
    {
        [Column(name: "ID")][Key] public int Id { get; set; }
        [ForeignKey(name: "Folder-ID")] public int FolderId { get; set; }
    }
}