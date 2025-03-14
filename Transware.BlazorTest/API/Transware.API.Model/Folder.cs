using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transware.API.Model
{
    public class Folder
    {
        public int Id { get; set; }
        public Folder ParentFolder { get; set; }
        public string Name { get; set; }
    }
}
