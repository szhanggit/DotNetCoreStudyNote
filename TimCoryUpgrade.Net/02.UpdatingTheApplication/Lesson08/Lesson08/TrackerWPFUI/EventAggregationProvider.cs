using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerWPFUI
{
    public static class EventAggregationProvider
    {
        public static EventAggregator TrackerEventAggregor { get; set; } = new EventAggregator();
    }
}
