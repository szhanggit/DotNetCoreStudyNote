using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;

namespace Product.Api.Logging
{
    public class TelemetryInitializer : ITelemetryInitializer
    {
        private readonly IConfiguration _configuration;
        public TelemetryInitializer(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Initialize(ITelemetry telemetry)
        {
            telemetry.Context.Component.Version = _configuration["ApplicationInsights:AppVersion"];

            if (string.IsNullOrEmpty(telemetry.Context.Cloud.RoleName))
                telemetry.Context.Cloud.RoleName = _configuration["ApplicationInsights:RoleName"];
        }
    }
}
