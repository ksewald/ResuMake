using ResumMake.Business;
using System;
using System.Linq;
using System.Reflection;

namespace ResumMake.Utils
{
    public class DynamicFieldBase
    {
        public bool TryGetDynamicFieldKey(Type T, out string keyValue)
        {
            keyValue = null;
            DynamicFieldKey keyAttribute = (DynamicFieldKey)Attribute.GetCustomAttribute(T, typeof(DynamicFieldKey));

            if (string.IsNullOrWhiteSpace(keyAttribute?.KeyValue)) return false;

            keyValue = keyAttribute?.KeyValue;
            return true;
        }

        public bool TryGetDynamicFieldKey(Type T, string propertyName, out string keyValue)
        {
            keyValue = null;
            var keyAttribute = T.GetProperties()
                .FirstOrDefault(propInfo => propInfo.GetCustomAttribute<DynamicFieldKey>() != null && propInfo.Name == propertyName)
                ?.GetCustomAttribute<DynamicFieldKey>();


            //DynamicFieldKey keyAttribute = (DynamicFieldKey)Attribute.GetCustomAttribute(T, typeof(DynamicFieldKey));

            if (string.IsNullOrWhiteSpace(keyAttribute?.KeyValue)) return false;

            keyValue = keyAttribute?.KeyValue;
            return true;
        }

    }
}
