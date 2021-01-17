using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SGFramework.TypeDeclaration
{
    public class TypeDeclarationSyntaxReceiver : ITypeDeclarationSyntaxReceiver
    {
        private readonly List<(TypeDeclarationSyntax, AttributeSyntax)> _declarations = new();

        public IReadOnlyCollection<(TypeDeclarationSyntax, AttributeSyntax)> Declarations=> _declarations;

        public IAttributeContainsChecker AttributeContainsChecker { get; set; }

        public TypeDeclarationSyntaxReceiver( IAttributeContainsChecker attributeContainsChecker )
        {
            AttributeContainsChecker = attributeContainsChecker;
        }

        public void OnVisitSyntaxNode( SyntaxNode syntaxNode )
        {
            if( syntaxNode is not TypeDeclarationSyntax syntax || syntax.AttributeLists.Count == 0 )
            {
                return;
            }
            var suffixRegex = new Regex( @"Attribute$" );

            foreach( var attribute in syntax.AttributeLists.SelectMany( x => x.Attributes ) )
            {
                var name = attribute.Name.ToString();
                name = suffixRegex.Replace( name, "" );

                if( AttributeContainsChecker.ContainsAttribute( name ) )
                {
                    _declarations.Add( ( syntax, attribute ) );
                }
            }
        }
    }
}
