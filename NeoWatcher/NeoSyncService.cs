using Microsoft.AspNetCore.Http.HttpResults;
using Neo_Watcher;
using System.Text.Json;

public class NeoSyncService
{
    private readonly HttpClient _client;
    private readonly NeoContext _context;

    public NeoSyncService(HttpClient client, NeoContext context)
    {
        _client = client;
        _context = context;
    }

    public async Task FetchAndSyncAsync()
    {
        var start = DateTime.UtcNow.Date.AddDays(-3);
        var end = DateTime.UtcNow.Date;

        var url = $"https://api.nasa.gov/neo/rest/v1/feed?start_date={start:2025-06-10}&end_date={end:2025-06-11}&api_key=J1aJ68odHL2bZefmVXGQhRnclbnUh5IxUDzoIWT2";
        var response = await _client.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var feed = JsonSerializer.Deserialize<NeoFeedResponse>(json);

        foreach (var day in feed.NearEarthObjects)
        {
            foreach (var obj in day.Value)
            {
                var date = DateTime.Parse(obj.CloseApproachData[0].CloseApproachDate);
                var id = obj.Id;

                var existing = await _context.NearEarthObjects.FindAsync(id);
                if (existing != null)
                    continue;

                var neo = new NearEarthObject
                {
                    Id = obj.Id,
                    Name = obj.Name,
                    CloseApproachDate = date,
                    EstimatedDiameterMin = obj.EstimatedDiameter.Meters.EstimatedDiameterMin,
                    EstimatedDiameterMax = obj.EstimatedDiameter.Meters.EstimatedDiameterMax,
                    IsPotentiallyHazardous = obj.IsHazardous,
                    RelativeVelocityKmh = double.Parse(obj.CloseApproachData[0].RelativeVelocity.KmH),
                    MissDistanceKm = double.Parse(obj.CloseApproachData[0].MissDistance.Kilometers)
                };

                _context.NearEarthObjects.Add(neo);
            }
        }

        await _context.SaveChangesAsync();
    }
}

