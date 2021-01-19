using System.Collections.Generic;
using System.Data;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SGFramework.Sample.ValueObject
{
    internal class ValueRangeAttributeArgumentParser : IAttributeArgumentParser
    {
        public void ParseAttributeArgument(
            int argumentIndex,
            SemanticModel semanticModel,
            ExpressionSyntax argumentExpression,
            IDictionary<AttributeParamName, object> result )
        {
            switch( argumentIndex )
            {
                case 0: CollectTypeOfSyntax( argumentExpression, semanticModel, AttributeParameterNames.Min, result ); break;
                case 1: CollectTypeOfSyntax( argumentExpression, semanticModel, AttributeParameterNames.Max, result ); break;
            }
        }

        private static void CollectTypeOfSyntax(
            ExpressionSyntax argumentExpr,
            SemanticModel semanticModel,
            AttributeParamName parameterName,
            IDictionary<AttributeParamName, object> result )
        {
            result[ parameterName ] = argumentExpr.ToString();
        }
    }
}
