using TXC.Proto.Product;

namespace Services.Extensions.Primitive
{
    public static class ProtoBufDecimalExtension
    {
        private const decimal NanoFactor = 1_000_000_000;

        public static decimal ToDecimal(this DecimalValue val)
        {
            return val.Units + val.Nanos / NanoFactor;
        }
    }
}
