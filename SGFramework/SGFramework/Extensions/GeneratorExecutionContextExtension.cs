using System.Linq;

using Microsoft.CodeAnalysis;

namespace SGFramework.Extension
{
    /// <summary>
    /// Reading msbuild items and properties
    /// <seealso href="https://jaylee.org/archive/2020/09/13/msbuild-items-and-properties-in-csharp9-sourcegenerators.html" />
    /// </summary>
    public static class GeneratorExecutionContextExtension
    {
        private const string SourceItemGroupMetadata = "build_metadata.AdditionalFiles.SourceItemGroup";

        // ReSharper disable once InconsistentNaming
        public static string GetMSBuildProperty(
            this GeneratorExecutionContext context,
            string name,
            string defaultValue = "" )
        {
            context.AnalyzerConfigOptions
                   .GlobalOptions
                   .TryGetValue( $"build_property.{name}", out var value );

            return value ?? defaultValue;
        }

        // ReSharper disable once InconsistentNaming
        public static string GetMSBuildPropertyOption(
            this GeneratorExecutionContext context,
            AdditionalText additionalText,
            string name,
            string defaultValue = "" )
        {
            context.AnalyzerConfigOptions
                   .GetOptions( additionalText )
                   .TryGetValue( $"build_metadata.additionalfiles.{name}", out var value );

            return value ?? defaultValue;
        }

        // ReSharper disable once InconsistentNaming
        public static string[] GetMSBuildItems( this GeneratorExecutionContext context, string name ) =>
            context
               .AdditionalFiles
               .Where( f =>
                           context.AnalyzerConfigOptions
                                  .GetOptions( f )
                                  .TryGetValue( SourceItemGroupMetadata, out var sourceItemGroup )
                           && sourceItemGroup == name
                )
               .Select( f => f.Path )
               .ToArray();
    }
}
