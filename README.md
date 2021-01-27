SGFramework
=================



Lightweight C#9.0 SourceGenerator Framework.



## Feature

- Wrapper of Microsoft.CodeAnalysis.ISourceGenerator
    - A library for parsing attributes of type declarations in a few steps
    - Provides a constant processing flow, allowing you to focus on implementing the necessary processing

## Installation

Install from nuget (serach by "SGFramework" )

## What code do I need to implement?

### Attribute Parser

Implement a parser per attribute with IAttributeArgumentParser.

```C#
using SGFramework;
using SGFramework.TypeDeclaration;

namespace Sample
{
    internal class MyAttributeArgumentParser : IAttributeArgumentParser
    {
        public void ParseAttributeArgument(
            int argumentIndex,
            AttributeArgumentSyntax argumentSyntax,
            SemanticModel semanticModel,
            ExpressionSyntax argumentExpression,
            IDictionary<AttributeParamName, object> result )
        {
            //
            // argumentIndex is index of attribute parameter [My(arg1,arg2)]
            //
            switch( argumentIndex )
            {
                // e.g. [My(100, 200)]
                case 0:
                    result[ arg1 ] = argumentExpression.ToString(); // 100
                    break;
                case 1:
                    result[ arg2 ] = argumentExpression.ToString(); // 200
                    break;
            }
        }
    }
}
```



### Generator

Implement a SourceGenerator subclass

```c#
using Microsoft.CodeAnalysis;

using SGFramework;
using SGFramework.TypeDeclaration;

namespace Sample
{
    [Generator] // Must set a  Microsoft.CodeAnalysis.Generator attribute
    internal class SampleGenerator : SourceGenerator<TypeDeclarationSyntaxReceiver>
    {
#if DEBUG
        // if true, launch JIT Debugger
        protected override bool LaunchDebuggerOnInit => false;
#endif

        protected override TypeDeclarationSyntaxReceiver CreateReceiver()
            => new( this );

        protected override void SetupAttributeArgumentParser( Dictionary<AttributeTypeName, IAttributeArgumentParser> map )
        {
            // Mapping inplementation of IAttributeArgumentParser with attribute name
            map[ new AttributeTypeName( "MyAttribute" ) ] = new MyAttributeArgumentParser();
        }

        protected override void GenerateAttributeCode( GeneratorExecutionContext context )
        {
            // Mapping Attribute code on memory with GeneratorExecutionContext
            context.AddSource( hintName1, code );
            context.AddSource( hintName2, code );
            :
            :
        }

        protected override string GenerateCode(
            TypeDeclarationSyntax declaration,
            string nameSpace,
            string typeName,
            IDictionary<AttributeTypeName, IDictionary<AttributeParamName, object>> attributeTypeList )
        {
            // Generating your code with T4 etc
            return code;
        }
    }
}
```



## Reference your generator project from other C# Project

if reference by `PrijectReference`, write following

```xml
    <ItemGroup>
        <ProjectReference Include="..\Generator\Generator.csproj" OutputItemType="Analyzer" />
    </ItemGroup>
```

note:  attribute `ReferenceOutputAssembly` value  needs **true** (if set to false,analzer may not work)



## Reference your generator (Nuget package) from other C# Project

```xml
    <ItemGroup>
        <PackageReference Include="Generator" Version="0.0.1" />
    </ItemGroup>
```

Same as nuget package reference.



## Implemented by SGFramrwork

- ValueObjectGenerator
    - https://github.com/r-koubou/ValueObjectGenerator