using Microsoft.CodeAnalysis;

namespace SGFramework
{
    public interface IGenerator : ISourceGenerator
    {
        public bool LaunchDebuggerOnInit { get; }
    }
}
