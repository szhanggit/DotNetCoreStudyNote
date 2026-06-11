using MSWindowsService_0;
using System;
using Xunit;

namespace TestMSWindowsService
{
    public class TestLoggingService
    {
        [Fact]
        public void TestLog()
        {
            LoggingService ls = new LoggingService();
            ls.Log("This is my test.");
        }
    }
}
