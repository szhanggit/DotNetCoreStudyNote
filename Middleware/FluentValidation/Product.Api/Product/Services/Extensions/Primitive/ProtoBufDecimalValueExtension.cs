
using TXC.Proto.Product;

namespace Services.Extensions.Primitive
{
    public static class ProtoBufDecimalValueExtension
    {
        private const decimal NanoFactor = 1_000_000_000;

        public static DecimalValue ToDecimalValue(this decimal val)
        {
            var units = decimal.ToInt64(val);
            var nanos = decimal.ToInt32((val - units) * NanoFactor);

            return new DecimalValue
            {
                Units = units,
                Nanos = nanos
            };
        }

        public static DecimalValue ToDecimalValue(this decimal? val)
        {
            if (val == null)
            {
                return null;
            }

            var dec = (decimal)val;

            var units = decimal.ToInt64(dec);
            var nanos = decimal.ToInt32((dec - units) * NanoFactor);

            return new DecimalValue
            {
                Units = units,
                Nanos = nanos
            };
        }
    }
}
