using System.Collections.Generic;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SGFramework.Sample.ValueObject
{
    internal class ValueRangeAttributeArgumentParser : IAttributeArgumentParser
    {
        public void ParseAttributeArgument(
            int argumentIndex,
            AttributeArgumentSyntax argumentSyntax,
            SemanticModel semanticModel,
            ExpressionSyntax argumentExpression,
            IDictionary<AttributeParamName, object> result )
        {
            switch( argumentIndex )
            {
                case 0:
                    AttributeArgumentParserHelper.ParseExpression(
                        argumentExpression,
                        result,
                        AttributeParameterNames.Min
                    );
                    break;
                case 1:
                    AttributeArgumentParserHelper.ParseExpression(
                        argumentExpression,
                        result,
                        AttributeParameterNames.Max
                    );
                    break;
            }
        }
    }
}
