using System.Text;
using PWC_Cs.Core.Models;

namespace PWC_Cs.Web.Services;

public class PrzmVvwmInputWriter
{
    private static string Safe(object? o) => o?.ToString()?.Trim() ?? string.Empty;

    private static void WriteCsvLine(StreamWriter writer, params object?[] parts)
    {
        writer.WriteLine(string.Join(",", parts.Select(Safe)) + ",");
    }

    private static void WriteCsvLineNoTrailComma(StreamWriter writer, params object?[] parts)
    {
        writer.WriteLine(string.Join(",", parts.Select(Safe)));
    }

    public async Task WriteAsync(ProjectModel project, string filePath)
    {
        using var sw = new StreamWriter(filePath, false, Encoding.UTF8);

        // --- Header / project info ---
        WriteCsvLine(sw, "PWC Version 4.0 C#");
        WriteCsvLine(sw, project.WorkingDirectory);
        WriteCsvLine(sw, project.FamilyName);
        WriteCsvLineNoTrailComma(sw, project.WeatherFileDirectory);
        WriteCsvLine(sw, project.AdvancedSettings.WaterbodyEvaporationCoefficient ?? "1.0");

        // --- Chemical flags and counts ---
        WriteCsvLine(sw,
            project.ChemicalInputs.SorptionType == "Koc",
            false,   // UseFreundlich - will wire later
            false,   // UseNonequilibrium - will wire later
            false,   // PoundToKiloConversion - will wire later
            false    // IsHydrolysisOverride - will wire later
        );

        WriteCsvLine(sw, 1); // nchem placeholder for now

        // --- Chemical section (one chemical set for now) ---
        WriteCsvLine(sw, project.ChemicalInputs.SorptionParent, project.ChemicalInputs.SorptionDaughter, project.ChemicalInputs.SorptionGranddaughter);
        WriteCsvLine(sw, project.AdvancedSettings.N1Parent, project.AdvancedSettings.N1Daughter, project.AdvancedSettings.N1Granddaughter);
        WriteCsvLine(sw, project.AdvancedSettings.Kf2Parent, project.AdvancedSettings.Kf2Daughter, project.AdvancedSettings.Kf2Granddaughter);
        WriteCsvLine(sw, project.AdvancedSettings.N2Parent, project.AdvancedSettings.N2Daughter, project.AdvancedSettings.N2Granddaughter);
        WriteCsvLine(sw, project.AdvancedSettings.KsParent, project.AdvancedSettings.KsDaughter, project.AdvancedSettings.KsGranddaughter);
        WriteCsvLine(sw, project.AdvancedSettings.LowestConcentrationForFreundlichMgL, project.AdvancedSettings.SubTimeSteps);




        WriteCsvLine(sw, project.ChemicalInputs.WaterColumnHalflifeParent, project.ChemicalInputs.WaterColumnHalflifeDaughter, project.ChemicalInputs.WaterColumnHalflifeGranddaughter);
        WriteCsvLine(sw, project.ChemicalInputs.WaterReferenceTemperatureParent, project.ChemicalInputs.WaterReferenceTemperatureDaughter, project.ChemicalInputs.WaterReferenceTemperatureGranddaughter);
        WriteCsvLine(sw, project.ChemicalInputs.BenthicHalflifeParent, project.ChemicalInputs.BenthicHalflifeDaughter, project.ChemicalInputs.BenthicHalflifeGranddaughter);
        WriteCsvLine(sw, project.ChemicalInputs.BenthicReferenceTemperatureParent, project.ChemicalInputs.BenthicReferenceTemperatureDaughter, project.ChemicalInputs.BenthicReferenceTemperatureGranddaughter);
        WriteCsvLine(sw, project.ChemicalInputs.PhotoHalflifeParent, project.ChemicalInputs.PhotoHalflifeDaughter, project.ChemicalInputs.PhotoHalflifeGranddaughter);
        WriteCsvLine(sw, project.ChemicalInputs.PhotoReferenceLatitudeParent, project.ChemicalInputs.PhotoReferenceLatitudeDaughter, project.ChemicalInputs.PhotoReferenceLatitudeGranddaughter);
        WriteCsvLine(sw, project.ChemicalInputs.HydrolysisHalflifeParent, project.ChemicalInputs.HydrolysisHalflifeDaughter, project.ChemicalInputs.HydrolysisHalflifeGranddaughter);
        WriteCsvLine(sw, project.ChemicalInputs.HydrolysisReferenceTemperatureParent, project.ChemicalInputs.HydrolysisReferenceTemperatureDaughter, project.ChemicalInputs.HydrolysisReferenceTemperatureGranddaughter);
        WriteCsvLine(sw, project.ChemicalInputs.SoilHalflifeParent, project.ChemicalInputs.SoilHalflifeDaughter, project.ChemicalInputs.SoilHalflifeGranddaughter);
        WriteCsvLine(sw, project.ChemicalInputs.SoilReferenceTemperatureParent, project.ChemicalInputs.SoilReferenceTemperatureDaughter, project.ChemicalInputs.SoilReferenceTemperatureGranddaughter);
        WriteCsvLine(sw, project.ChemicalInputs.FoliarHalflifeParent, project.ChemicalInputs.FoliarHalflifeDaughter, project.ChemicalInputs.FoliarHalflifeGranddaughter);
        WriteCsvLine(sw, project.ChemicalInputs.FoliarWashoffParent, project.ChemicalInputs.FoliarWashoffDaughter, project.ChemicalInputs.FoliarWashoffGranddaughter);
        WriteCsvLine(sw, project.ChemicalInputs.MWTParent, project.ChemicalInputs.MWTDaughter, project.ChemicalInputs.MWTGranddaughter);
        WriteCsvLine(sw, project.ChemicalInputs.VaporPressureParent, project.ChemicalInputs.VaporPressureDaughter, project.ChemicalInputs.VaporPressureGranddaughter);
        WriteCsvLine(sw, project.ChemicalInputs.SolubilityParent, project.ChemicalInputs.SolubilityDaughter, project.ChemicalInputs.SolubilityGranddaughter);
        WriteCsvLine(sw, project.ChemicalInputs.HenryParent, project.ChemicalInputs.HenryDaughter, project.ChemicalInputs.HenryGranddaughter);
        WriteCsvLine(sw, project.ChemicalInputs.AirDiffusionParent, project.ChemicalInputs.AirDiffusionDaughter, project.ChemicalInputs.AirDiffusionGranddaughter);
        WriteCsvLine(sw, project.ChemicalInputs.HeatHenryParent, project.ChemicalInputs.HeatHenryDaughter, project.ChemicalInputs.HeatHenryGranddaughter);

        // TODO: expand with advanced settings, schemes, waterbody, and output sections next
    }
}