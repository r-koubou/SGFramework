using System;
using System.Collections.Generic;
using System.Diagnostics;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SGFramework.TypeDeclaration
{
    public abstract class SourceGenerator<TReceiver> : ISourceGenerator
        where TReceiver : ITypeDeclarationSyntaxReceiver
    {
        protected abstract bool LaunchDebuggerOnInit { get; }

        protected Dictionary<string, IAttributeArgumentParser> AttributeArgumentParsers { get; } = new();

        public virtual void Initialize( GeneratorInitializationContext context )
        {
#if DEBUG
            if( LaunchDebuggerOnInit && !Debugger.IsAttached )
            {
                Debugger.Launch();
            }
#endif
            SetupAttributeArgumentParser( AttributeArgumentParsers );

            var receiver = CreateReceiver();
            context.RegisterForSyntaxNotifications( () => receiver );
        }

        protected abstract TReceiver CreateReceiver();

        protected abstract void SetupAttributeArgumentParser( Dictionary<string, IAttributeArgumentParser> map );

        protected static void GenerateAttribute( GeneratorExecutionContext context ) {}

        public virtual void Execute( GeneratorExecutionContext context )
        {
            try
            {
                var receiver = context.SyntaxReceiver is TReceiver syntaxReceiver ? syntaxReceiver : default;
                if( receiver == null )
                {
                    return;
                }

                var declarations = CollectDeclarations( context, receiver );

            }
            catch( Exception e )
            {
                Trace.WriteLine( e );
            }

        }

        #region Collect
        private IReadOnlyCollection<TypeDeclarationContext> CollectDeclarations(
            GeneratorExecutionContext context,
            TReceiver receiver )
        {
            var result = new List<TypeDeclarationContext>();

            foreach( var (declaration, attribute) in receiver.Declarations )
            {
                if( attribute.ArgumentList is null )
                {
                    result.Add( new TypeDeclarationContext( context, declaration ) );
                    continue;
                }

                var ctx = new TypeDeclarationContext( context, declaration );
                var attributeName = attribute.Name.ToString();

                for( var index = 0; index < attribute.ArgumentList.Arguments.Count; index++ )
                {
                    var argument = attribute.ArgumentList.Arguments[ index ];
                    var argumentExpression = argument.Expression;

                    if( !AttributeArgumentParsers.TryGetValue( attributeName, out var parser ) )
                    {
                        continue;
                    }

                    parser.ParseAttributeArgument( ctx, index, argumentExpression );
                }

                result.Add( ctx );
            }

            return result;
        }

        protected abstract void ParseAttributeArgumentExpression( TypeDeclarationContext context, string attributeName, int argumentIndex, ExpressionSyntax expression );

        #endregion

        #region Utility
        public static TEnum ParseEnumValue<TEnum>( ExpressionSyntax expression ) where TEnum : Enum
        {
            var enumValue = Enum.Parse(
                typeof(TEnum),
                expression.ToString()
                          .Replace( $"{nameof(TEnum)}.", "" )
                          .Replace( "|",                 "," ) );

            return (TEnum)enumValue;
        }
        #endregion
    }
}
