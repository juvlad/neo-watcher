public class NeoSyncJob : BackgroundService
{
    private readonly IServiceProvider _provider;

    public NeoSyncJob(IServiceProvider provider) => _provider = provider;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _provider.CreateScope();
            var sync = scope.ServiceProvider.GetRequiredService<NeoSyncService>();

            try
            {
                await sync.FetchAndSyncAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");// логируем ошибку
            }

            await Task.Delay(TimeSpan.FromHours(12), stoppingToken);
        }
    }
}
