using Microsoft.CodeAnalysis;

namespace SGFramework
{
    public interface ISyntaxReceiverSupport<out TReceiver> where TReceiver : ISyntaxReceiver
    {
        public TReceiver CreateSyntaxReceiver();
    }
}
