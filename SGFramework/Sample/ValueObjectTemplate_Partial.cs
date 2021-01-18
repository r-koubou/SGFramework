namespace SGFramework.Sample
{
    internal partial class ValueObjectTemplate
    {
        internal string Namespace { get; set; } = string.Empty;
        internal string Name { get; set; } = string.Empty;
        internal string BaseTypeName { get; set; } = string.Empty;
        internal ValueOption ValueOption { get; set; } = ValueOption.None;
    }
}
