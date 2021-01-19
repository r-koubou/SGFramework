using System.Collections.Generic;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SGFramework.TypeDeclaration;

namespace SGFramework.Sample.ValueObject
{
    [Generator]
    internal class Sample : SourceGenerator<TypeDeclarationSyntaxReceiver>, IAttributeContainsChecker
    {
        private static readonly AttributeTypeName ValueObjectAttributeTypeName = new ( "ValueObject" );

#if DEBUG
        protected override bool LaunchDebuggerOnInit => false;
#else
        protected override bool LaunchDebuggerOnInit => false;
#endif

        protected override TypeDeclarationSyntaxReceiver CreateReceiver()
            => new( this );

        protected override void SetupAttributeArgumentParser( Dictionary<AttributeTypeName, IAttributeArgumentParser> map )
        {
            map[ ValueObjectAttributeTypeName ] = new ValueObjectAttributeArgumentParser();
        }

        public bool ContainsAttribute( AttributeTypeName attributeTypeName )
            => attributeTypeName == ValueObjectAttributeTypeName;

        protected override void GenerateAttributeCode( GeneratorExecutionContext context )
        {
            context.AddSource(
                ValueObjectAttributeTypeName.Value,
                new ValueObjectAttributeTemplate().TransformText()
            );
        }

        protected override string GenerateCode(
            TypeDeclarationSyntax declaration,
            string nameSpace,
            string typeName,
            IDictionary<AttributeTypeName, IDictionary<AttributeParamName, object>> attributeTypeList )
        {
            if( !attributeTypeList.TryGetValue( ValueObjectAttributeTypeName, out var attributeParams ))
            {
                return string.Empty;
            }

            if( !attributeParams.TryGetValue( AttributeParameterNames.BaseName, out var baseName ) )
            {
                return string.Empty;
            }

            if( !attributeParams.TryGetValue( AttributeParameterNames.OptionFlags, out var valueOption ) )
            {
                valueOption = ValueOption.None;
            }

            var template = new ValueObjectTemplate()
            {
                Namespace    = nameSpace,
                Name         = typeName,
                BaseTypeName = (string)baseName,
                ValueOption  = (ValueOption)valueOption
            };

            var code = template.TransformText();

            return code;
        }
    }
}
