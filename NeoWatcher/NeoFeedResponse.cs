using System.Text.Json.Serialization;

public class NeoFeedResponse
{
    [JsonPropertyName("near_earth_objects")]
    public Dictionary<string, List<NeoObject>> NearEarthObjects { get; set; }
}

public class NeoObject
{
    public string Id { get; set; }
    public string Name { get; set; }

    [JsonPropertyName("is_potentially_hazardous_asteroid")]
    public bool IsHazardous { get; set; }

    [JsonPropertyName("estimated_diameter")]
    public DiameterWrapper EstimatedDiameter { get; set; }

    [JsonPropertyName("close_approach_data")]
    public List<CloseApproachData> CloseApproachData { get; set; }
}

public class DiameterWrapper
{
    public DiameterMeters Meters { get; set; }
}

public class DiameterMeters
{
    public double EstimatedDiameterMin { get; set; }
    public double EstimatedDiameterMax { get; set; }
}

public class CloseApproachData
{
    public string CloseApproachDate { get; set; }
    public RelativeVelocity RelativeVelocity { get; set; }
    public MissDistance MissDistance { get; set; }
}

public class RelativeVelocity
{
    [JsonPropertyName("kilometers_per_hour")]
    public string KmH { get; set; }
}

public class MissDistance
{
    public string Kilometers { get; set; }
}
