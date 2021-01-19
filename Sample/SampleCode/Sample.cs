using SGFramework.Sample.ValueObject;

namespace SampleCode
{
    [ValueObject( typeof(int))]
    [ValueRange(0, 100+10)]
    public partial class Demo
    {}
}
