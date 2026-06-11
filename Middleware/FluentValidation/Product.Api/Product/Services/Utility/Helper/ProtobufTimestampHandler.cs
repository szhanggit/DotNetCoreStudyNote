using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Utility.Helper
{
    public class ProtobufTimestampHandler : SqlMapper.TypeHandler<Google.Protobuf.WellKnownTypes.Timestamp>
    {
        public override void SetValue(IDbDataParameter parameter, Google.Protobuf.WellKnownTypes.Timestamp value)
        {
            parameter.Value = value;
        }

        public override Google.Protobuf.WellKnownTypes.Timestamp Parse(object value)
        {
            return Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.SpecifyKind((DateTime)value, DateTimeKind.Utc));
        }
    }
}
