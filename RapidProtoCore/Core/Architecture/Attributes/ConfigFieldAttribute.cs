using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RapidProtoCore.Interfaces.Attributes;

namespace RapidProtoCore.Core.Architecture.Attributes
{
    public sealed class ConfigFieldAttribute : Attribute, IPrimaryCategoryAttribute
    {
    }

    public class ListViewFieldAttribute : Attribute, IPrimaryCategoryAttribute
    {
    }
}
