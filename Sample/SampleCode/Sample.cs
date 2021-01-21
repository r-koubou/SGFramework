using ValueObjectGenerator;

namespace SampleCode
{
    [ValueObject( typeof(int), ValueName = "DemoValue")]
    [ValueRange(0, 100+10)]
    public partial class Demo
    {}
}
