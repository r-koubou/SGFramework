using Microsoft.CodeAnalysis.CSharp.Syntax;

using SGFramework.TypeDeclaration;

namespace SGFramework
{
    public interface IAttributeArgumentParser
    {
        public void ParseAttributeArgument( TypeDeclarationContext context, int index, ExpressionSyntax expression );
    }
}
