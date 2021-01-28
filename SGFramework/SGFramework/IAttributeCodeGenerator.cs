using System.Collections.Generic;

using Microsoft.CodeAnalysis;

namespace SGFramework
{
    public interface IAttributeCodeGenerator
    {
        public void SetupAttributeArgumentParser( Dictionary<AttributeTypeName, IAttributeArgumentParser> map );
        public void GenerateAttributeCode( GeneratorExecutionContext context );
    }
}
