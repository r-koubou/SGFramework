using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SGFramework.TypeDeclaration;

namespace SGFramework.Sample
{
    [Generator]
    internal class Sample : SourceGenerator<TypeDeclarationSyntaxReceiver>, IAttributeContainsChecker
    {
        private const string ValueObjectAttributeName = "ValueObject";

        protected override bool LaunchDebuggerOnInit { get; } = false;

        protected override TypeDeclarationSyntaxReceiver CreateReceiver()
            => new( this );

        protected override void SetupAttributeArgumentParser( Dictionary<string, IAttributeArgumentParser> map )
        {
            map[ nameof(ValueObjectAttributeArgumentParser) ] = new ValueObjectAttributeArgumentParser();
        }

        public bool ContainsAttribute( string attributeName )
            => attributeName == ValueObjectAttributeName;

        protected override void GenerateAttributeCode( GeneratorExecutionContext context )
        {
            context.AddSource(
                ValueObjectAttributeName,
                new ValueObjectAttributeTemplate().TransformText()
            );
        }

        protected override string GenerateCode(
            TypeDeclarationSyntax declaration,
            string nameSpace,
            string typeName,
            IReadOnlyList<AttributeProperties> propertiesList )
        {
            if( !propertiesList.Any() )
            {
                return string.Empty;
            }

            var attributeValue = propertiesList.First().Propertes;

            var template = new ValueObjectTemplate()
            {
                Namespace    = nameSpace,
                Name         = typeName,
                BaseTypeName = (string)attributeValue[ AttributePropertyKey.AttributeBaseName ],
                ValueOption  = (ValueOption)attributeValue[ AttributePropertyKey.AttributeOptionFlags ]
            };

            var code = template.TransformText();

            return code;
        }
    }
}
