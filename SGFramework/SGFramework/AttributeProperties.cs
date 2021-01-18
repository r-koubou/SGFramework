using System.Collections.Generic;

namespace SGFramework
{
    public class AttributeProperties
    {
        public string AttributeType { get; }

        public Dictionary<string, object> Propertes { get; } = new();

        public AttributeProperties( string attributeType )
        {
            AttributeType = attributeType;
        }
    }
}
