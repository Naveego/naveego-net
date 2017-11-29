using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Naveego.Json
{
    public class NaveegoJsonContractResolver : CamelCasePropertyNamesContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var jsonProperty = base.CreateProperty(member, memberSerialization);
            
            if(jsonProperty.PropertyType == typeof(ResourceMetadata))
            {
                jsonProperty.PropertyName = "_meta";
                jsonProperty.Order = -1;
            }

            return jsonProperty;

        }
    }
}
