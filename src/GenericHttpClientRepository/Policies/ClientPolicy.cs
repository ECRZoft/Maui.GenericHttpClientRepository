using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using System.Diagnostics;

namespace GenericHttpClientRepository.Policies;
public class ClientPolicy
{
    private readonly ILogger<ClientPolicy> _logger;

    public AsyncRetryPolicy<HttpResponseMessage> ImmediateHttpRetry { get; }
    public AsyncRetryPolicy<HttpResponseMessage> LinearHttpRetry { get; }
    public AsyncRetryPolicy<HttpResponseMessage> ExponentialHttpRetry { get; }
    public AsyncRetryPolicy<HttpResponseMessage> LoggingExponentialHttpRetry { get; }

    public ClientPolicy(ILogger<ClientPolicy> logger)
    {
        _logger = logger;

        ImmediateHttpRetry = Policy.HandleResult<HttpResponseMessage>(
            res => !res.IsSuccessStatusCode)
            .RetryAsync(10);

        LinearHttpRetry = Policy.HandleResult<HttpResponseMessage>(
            res => !res.IsSuccessStatusCode)
            .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(3));

        ExponentialHttpRetry = Policy.HandleResult<HttpResponseMessage>(
            res => !res.IsSuccessStatusCode)
            .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

        LoggingExponentialHttpRetry = Policy.HandleResult<HttpResponseMessage>(
            res => !res.IsSuccessStatusCode)
            .WaitAndRetryAsync
            (
                retryCount: 5,
                sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                onRetry: (ex, time) =>
                {
                    _logger!.LogError($"--> TimeSpan: {time.TotalSeconds}");
                }
            );

    }
}