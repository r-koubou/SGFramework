using System;
using System.Collections.Generic;

namespace SGFramework
{
    public class AttributeProperties
    {
        public Type AttributeType { get; }

        public Dictionary<string, object> Propertes { get; } = new();

        public AttributeProperties( Type attributeType )
        {
            AttributeType = attributeType;
        }
    }
}
