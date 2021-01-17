using System.Collections.Generic;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SGFramework.TypeDeclaration
{
    public interface ITypeDeclarationSyntaxReceiver : ISyntaxReceiver
    {
        public IReadOnlyCollection<(TypeDeclarationSyntax, AttributeSyntax)> Declarations { get; }
        public IAttributeContainsChecker AttributeContainsChecker { get; set; }
    }
}
