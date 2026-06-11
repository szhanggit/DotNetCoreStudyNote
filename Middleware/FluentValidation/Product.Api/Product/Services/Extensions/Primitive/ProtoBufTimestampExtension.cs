using System;

namespace Services.Extensions.Primitive
{
    public static class ProtoBufTimestampExtension
    {
        public static Google.Protobuf.WellKnownTypes.Timestamp ConvertToProtoTimeStamp(this DateTime value)
        { 
            return Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.SpecifyKind(value,DateTimeKind.Utc));
        }

        public static Google.Protobuf.WellKnownTypes.Timestamp ConvertToProtoTimeStamp(this DateTime? value)
        {
            return value == null ? null : Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.SpecifyKind(value.Value, DateTimeKind.Utc));
        }
    }
}
