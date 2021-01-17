using System.Collections.Generic;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SGFramework.TypeDeclaration
{
    public class TypeDeclarationContext
    {
        public TypeDeclarationSyntax Syntax { get; }
        public SemanticModel SemanticModel { get; }

        public List<AttributeProperties> Properties { get; } = new();

        public TypeDeclarationContext( GeneratorExecutionContext ctx, TypeDeclarationSyntax syntax )
        {
            Syntax        = syntax;
            SemanticModel = ctx.Compilation.GetSemanticModel( syntax.SyntaxTree );
        }
    }
}
