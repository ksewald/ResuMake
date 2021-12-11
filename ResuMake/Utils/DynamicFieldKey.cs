using System;

namespace ResumMake.Business
{
    [AttributeUsage(AttributeTargets.Property)
]
    public class DynamicFieldKey : Attribute
    {
        public string KeyValue { get; }

        public DynamicFieldKey(string key)
        {
            KeyValue = key;
        }
    }
}
