namespace PWC_Cs.Core.Models;

public class AdvancedSettingsModel
{
    public bool AdjustCnForSoilMoisture { get; set; }
    public bool UseFreundlich { get; set; }
    public bool UseNonequilibrium { get; set; }
    public string? Q10 { get; set; } = "2.0";
    public string SubTimeSteps { get; set; } = "1";
    public string LowestConcentrationForFreundlichMgL { get; set; } = "0.0001";

    public string? N1Parent { get; set; }
    public string? N1Daughter { get; set; }
    public string? N1Granddaughter { get; set; }
    public string? Kf2Parent { get; set; }
    public string? Kf2Daughter { get; set; }
    public string? Kf2Granddaughter { get; set; }
    public string? N2Parent { get; set; }
    public string? N2Daughter { get; set; }
    public string? N2Granddaughter { get; set; }
    public string? KsParent { get; set; }
    public string? KsDaughter { get; set; }
    public string? KsGranddaughter { get; set; }




}