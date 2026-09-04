namespace PWC_Cs.Core.Models;

public class WaterbodyModel
{
    public bool UseEpaReservoir { get; set; }
    public bool UseEpaPond { get; set; }
    public bool UseEpaTpezWpez { get; set; }
    public bool UseSprayBuffersForTpez { get; set; }

    public List<string> SpecialWaterbodies { get; set; } = new();
}
