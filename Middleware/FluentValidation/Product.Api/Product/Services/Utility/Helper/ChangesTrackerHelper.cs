using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Utility.Helper
{
    public interface IChangesTrackerHelper<T1, T2>
    {
        IEnumerable<T2> AddedItems { get; }
        IEnumerable<T1> RemovedItems { get; }
        IEnumerable<T1> UpdatedItems { get; }
    }
    public class ChangesTrackerHelper<T1, T2> : IChangesTrackerHelper<T1, T2>
    {
        private readonly IEnumerable<T1> oldValues;
        private readonly IEnumerable<T2> newValues;
        private readonly Func<T1, T2, bool> areEqual;

        public ChangesTrackerHelper(IEnumerable<T1> oldValues, IEnumerable<T2> newValues, Func<T1, T2, bool> areEqual)
        {
            this.oldValues = oldValues;
            this.newValues = newValues;
            this.areEqual = areEqual;
        }

        public IEnumerable<T2> AddedItems
        {
            get => newValues.Where(n => oldValues.All(o => !areEqual(o, n)));
        }

        public IEnumerable<T1> RemovedItems
        {
            get => oldValues.Where(n => newValues.All(o => !areEqual(n, o)));
        }

        public IEnumerable<T1> UpdatedItems
        {
            get => oldValues.Where(n => newValues.Any(o => areEqual(n, o)));
        }
    }

}
