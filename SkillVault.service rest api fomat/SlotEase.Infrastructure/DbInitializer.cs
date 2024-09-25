namespace SlotEase.Infrastructure;

public class DbInitializer
{
    private readonly ILogger<DbInitializer> _logger;

    private readonly IHostEnvironment _hostEnvironment;
    private readonly SlotEaseContext _context;

    public DbInitializer(ILogger<DbInitializer> logger, IHostEnvironment hostEnvironment, SlotEaseContext context)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _hostEnvironment = hostEnvironment ?? throw new ArgumentNullException(nameof(hostEnvironment));
        _context = context;
    }

    public async Task SeedAsync(int retries = 3)
    {
        /*
        var policy = CreatePolicy(retries, nameof(ClientspaceContext));

        await policy.ExecuteAsync(async () =>
        {
            // TODO call seed methods here
        });*/

    }


    private AsyncRetryPolicy CreatePolicy(int retries, string prefix)
    {
        return Policy.Handle<Exception>().
            WaitAndRetryAsync(
                retryCount: retries,
                sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                onRetry: (exception, timeSpan, retry, ctx) =>
                {
                    _logger.LogWarning(exception, "[{prefix}] Exception {ExceptionType} with message {Message} detected on attempt {retry} of {retries}", prefix, exception.GetType().Name, exception.Message, retry, retries);
                }
            );
    }

}

