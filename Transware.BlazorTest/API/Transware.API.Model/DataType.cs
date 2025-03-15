using System.ComponentModel;

namespace Transware.API.Model
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
