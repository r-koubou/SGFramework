using System.Collections.Generic;
using System.Data;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SGFramework.Sample
{
    internal class ValueObjectAttributeArgumentParser : IAttributeArgumentParser
    {
        public void ParseAttributeArgument( int argumentIndex, SemanticModel semanticModel, ExpressionSyntax argumentExpression, List<AttributeProperties> result )
        {
            var property = new AttributeProperties( "ValueObjectAttribute" );

            switch( argumentIndex )
            {
                case 0: CollectTypeOfSyntax( argumentExpression, semanticModel, property ); break;
                case 1: CollectValueOptionSyntax( argumentExpression, property ); break;
            }

            result.Add( property );
        }

        private static void CollectTypeOfSyntax( ExpressionSyntax argumentExpr, SemanticModel semanticModel, AttributeProperties result )
        {
            if( argumentExpr is TypeOfExpressionSyntax typeExpression )
            {
                if( !( semanticModel.GetSymbolInfo( typeExpression.Type ).Symbol is ITypeSymbol symbol ) )
                {
                    throw new SyntaxErrorException( "type is missing" );
                }

                result.Propertes[ AttributePropertyKey.AttributeBaseName ] = symbol.ToString();
            }
        }

        private static void CollectValueOptionSyntax( ExpressionSyntax argumentExpr, AttributeProperties result )
        {
            var enumValue = SyntaxUtility.ParseEnumValue<ValueOption>( argumentExpr );
            result.Propertes[ AttributePropertyKey.AttributeOptionFlags ] = enumValue;
        }
    }
}
