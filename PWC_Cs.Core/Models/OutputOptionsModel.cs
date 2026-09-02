namespace PWC_Cs.Core.Models;

public class OutputOptionsModel
{
    public bool OutputRunoff { get; set; }
    public bool OutputErosion { get; set; }
    public bool OutputPestRunoff { get; set; }
    public bool OutputPestErosion { get; set; }
    public bool OutputConcLastLayer { get; set; }
    public bool OutputDailyFieldVolatilization { get; set; }
    public bool OutputDailyPestLeached { get; set; }
    public string? ChemInfiltrationDepth { get; set; }
    public bool OutputDecayedPest { get; set; }
    public string? OutputDecayDepth1 { get; set; }
    public string? OutputDecayDepth2 { get; set; }
    public bool OutputMassInSoilProfile { get; set; }
    public bool OutputMassSoilSpecific { get; set; }
    public string? OutputMassDepth1 { get; set; }
    public string? OutputMassDepth2 { get; set; }
    public bool OutputMassOnFoliage { get; set; }
    public bool OutputPrecipitation { get; set; }
    public bool OutputActualEvap { get; set; }
    public bool OutputTotalSoilWater { get; set; }
    public bool OutputIrrigation { get; set; }
    public bool OutputInfiltrationAtDepth { get; set; }
    public string? OutputInfiltrationDepth { get; set; }
    public bool OutputInfiltratedWaterLastLayer { get; set; }
    public bool OutputWaterConc { get; set; }
    public bool OutputSpraydrift { get; set; }
    public bool OutputGwBtc { get; set; }
    public bool CalculateEoF { get; set; }

    public List<AdditionalOutputRowModel> AdditionalOutputRows { get; set; } = new();
}
