using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Polly;

namespace Travel.Easy.Electric.Vehicles.Automation.Common
{
    public class RetryPolicy
    {
        private readonly IConfigurationRoot _configuration;

        private readonly int RetryCount;
        private readonly int RetryTimeInterval;

        public RetryPolicy(IConfigurationRoot configuration)
        {
            _configuration = configuration;
            RetryCount = _configuration.GetValue<int>("appSettings:retryCount");
            RetryTimeInterval = _configuration.GetValue<int>("appSettings:timeBetweenRetries");
        }
        public void Execute(Action action)
        {
            Policy
                .Handle<AssertionException>()
                .WaitAndRetry(RetryCount, retryAttempt => TimeSpan.FromSeconds(RetryTimeInterval), (exception, retryCount, context) => { TestContext.WriteLine($"Test retries {retryCount} times until success"); })
                .Execute(() =>
                {
                    using (new TestExecutionContext.IsolatedContext())
                    {
                        action();
                    }
                });
        }
    }
}
