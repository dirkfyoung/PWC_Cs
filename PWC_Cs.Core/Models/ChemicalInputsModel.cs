namespace PWC_Cs.Core.Models;

public class ChemicalInputsModel
{
    public string? ChemicalLevel { get; set; } = "Parent";

    public string? SorptionType { get; set; }

    public string? SorptionParent { get; set; }
    public string? SorptionDaughter { get; set; }
    public string? SorptionGranddaughter { get; set; }

    public string? WaterColumnHalflifeParent { get; set; }
    public string? WaterColumnHalflifeDaughter { get; set; }
    public string? WaterColumnHalflifeGranddaughter { get; set; }

    public string? WaterReferenceTemperatureParent { get; set; }
    public string? WaterReferenceTemperatureDaughter { get; set; }
    public string? WaterReferenceTemperatureGranddaughter { get; set; }

    public string? BenthicHalflifeParent { get; set; }
    public string? BenthicHalflifeDaughter { get; set; }
    public string? BenthicHalflifeGranddaughter { get; set; }

    public string? BenthicReferenceTemperatureParent { get; set; }
    public string? BenthicReferenceTemperatureDaughter { get; set; }
    public string? BenthicReferenceTemperatureGranddaughter { get; set; }

    public string? PhotoHalflifeParent { get; set; }
    public string? PhotoHalflifeDaughter { get; set; }
    public string? PhotoHalflifeGranddaughter { get; set; }

    public string? PhotoReferenceTemperatureParent { get; set; }
    public string? PhotoReferenceTemperatureDaughter { get; set; }
    public string? PhotoReferenceTemperatureGranddaughter { get; set; }

    public string? HydrolysisHalflifeParent { get; set; }
    public string? HydrolysisHalflifeDaughter { get; set; }
    public string? HydrolysisHalflifeGranddaughter { get; set; }

    public string? HydrolysisReferenceTemperatureParent { get; set; }
    public string? HydrolysisReferenceTemperatureDaughter { get; set; }
    public string? HydrolysisReferenceTemperatureGranddaughter { get; set; }

    public string? SoilHalflifeParent { get; set; }
    public string? SoilHalflifeDaughter { get; set; }
    public string? SoilHalflifeGranddaughter { get; set; }

    public string? SoilReferenceTemperatureParent { get; set; }
    public string? SoilReferenceTemperatureDaughter { get; set; }
    public string? SoilReferenceTemperatureGranddaughter { get; set; }

    public string? FoliarHalflifeParent { get; set; }
    public string? FoliarHalflifeDaughter { get; set; }
    public string? FoliarHalflifeGranddaughter { get; set; }

    public string? FoliarWashoffParent { get; set; }
    public string? FoliarWashoffDaughter { get; set; }
    public string? FoliarWashoffGranddaughter { get; set; }

    public string? MWTParent { get; set; }
    public string? MWTDaughter { get; set; }
    public string? MWTGranddaughter { get; set; }

    public string? VaporPressureParent { get; set; }
    public string? VaporPressureDaughter { get; set; }
    public string? VaporPressureGranddaughter { get; set; }

    public string? SolubilityParent { get; set; }
    public string? SolubilityDaughter { get; set; }
    public string? SolubilityGranddaughter { get; set; }

    public string? HenryParent { get; set; }
    public string? HenryDaughter { get; set; }
    public string? HenryGranddaughter { get; set; }

    public string? AirDiffusionParent { get; set; }
    public string? AirDiffusionDaughter { get; set; }
    public string? AirDiffusionGranddaughter { get; set; }

    public string? HeatHenryParent { get; set; }
    public string? HeatHenryDaughter { get; set; }
    public string? HeatHenryGranddaughter { get; set; }
}