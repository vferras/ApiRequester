namespace ApiRequester.XCutting.RetryPolicies
{
    using Polly;

    using System;

    public class RetryPolicyFactory
    {
        public static Policy GetPolicy(RetryPolicyEnum retryType, int retries, int seconds = 0)
        {
            switch (retryType)
            {
                case RetryPolicyEnum.SimpleRetry:
                    return Policy.Handle<Exception>().RetryAsync(retries);
                case RetryPolicyEnum.CircuitBreaker:
                    return Policy.Handle<Exception>().CircuitBreaker(retries, TimeSpan.FromSeconds(seconds));
                default:
                    return Policy.Handle<Exception>().RetryAsync(retries);
            }
            
        }
    }
}
