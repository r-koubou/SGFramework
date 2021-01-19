using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SGFramework.Sample.Value;
using SGFramework.TypeDeclaration;

namespace SGFramework.Sample
{
    [Generator]
    internal class Sample : SourceGenerator<TypeDeclarationSyntaxReceiver>, IAttributeContainsChecker
    {
        private static readonly AttributeName ValueObjectAttributeName = new ( "ValueObject" );

        protected override bool LaunchDebuggerOnInit { get; } = false;

        protected override TypeDeclarationSyntaxReceiver CreateReceiver()
            => new( this );

        protected override void SetupAttributeArgumentParser( Dictionary<AttributeName, IAttributeArgumentParser> map )
        {
            map[ ValueObjectAttributeName ] = new ValueObjectAttributeArgumentParser();
        }

        public bool ContainsAttribute( AttributeName attributeName )
            => attributeName == ValueObjectAttributeName;

        protected override void GenerateAttributeCode( GeneratorExecutionContext context )
        {
            context.AddSource(
                ValueObjectAttributeName.Value,
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

            //TODO 属性リストの可搬性（List(トークン出現順の決め打ち)→Dict(属性名ベースで逆引き)）
            var attributeValue = propertiesList.First().Propertes;

            var template = new ValueObjectTemplate()
            {
                Namespace    = nameSpace,
                Name         = typeName,
                BaseTypeName = (string)attributeValue[ AttributePropertyKey.AttributeBaseName ],
                ValueOption  = attributeValue.ContainsKey( AttributePropertyKey.AttributeOptionFlags )
                    ? (ValueOption)attributeValue[ AttributePropertyKey.AttributeOptionFlags ] : ValueOption.None
            };

            var code = template.TransformText();

            return code;
        }
    }
}
