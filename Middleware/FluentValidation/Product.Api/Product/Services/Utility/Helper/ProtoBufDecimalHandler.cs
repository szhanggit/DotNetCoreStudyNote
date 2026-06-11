using Dapper;
using Services.Extensions.Primitive;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TXC.Proto.Product;

namespace Services.Utility.Helper
{
    internal class ProtoBufDecimalHandler : SqlMapper.TypeHandler<DecimalValue>
    {
        public override void SetValue(IDbDataParameter parameter, DecimalValue value)
        {
            parameter.Value = value;
        }

        public override DecimalValue Parse(object value)
        {
            return ProtoBufDecimalValueExtension.ToDecimalValue((Decimal)value);
        }
    }
}

