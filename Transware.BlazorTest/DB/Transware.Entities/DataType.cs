using System.ComponentModel;

namespace Transware.Entities
{
    public enum DataType
    {
        [Description("string")]
        STRING,
        [Description("date")]
        DATE,
        [Description("int")]
        INT
    }
}
