using System;

using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SGFramework
{
    public static class SyntaxUtility
    {
        public static TEnum ParseEnumValue<TEnum>( ExpressionSyntax expression ) where TEnum : Enum
        {
            var enumValue = Enum.Parse(
                typeof(TEnum),
                expression.ToString()
                          .Replace( $"{nameof(TEnum)}.", "" )
                          .Replace( "|",                 "," ) );

            return (TEnum)enumValue;
        }
    }
}
