using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TXC.Common.MessageContract.SOC;

namespace Services.Utility.Helper
{
    public class ObjectService
    {
        public bool IsModified { get; set; }
        public List<CreateAuditTraceModel> createAuditTraceModel = new List<CreateAuditTraceModel> ();
        public T Assign<T>(T property, T value, string name)
        {
            if ((property != null && !property.Equals(value)) || (value != null && !value.Equals(property)))
            {
                IsModified = true;
                createAuditTraceModel.Add(new CreateAuditTraceModel { FieldName = name, OldValue = Convert.ToString(property), NewValue =Convert.ToString(value) });
            }

            return value;
        }
    }
}
