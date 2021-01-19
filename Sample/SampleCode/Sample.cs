using GeneratorSample;

namespace SampleCode
{
    [ValueObject(typeof(int))]
    public partial class Demo
    {
        private static partial int Validate( int value ) => value;
    }
}
