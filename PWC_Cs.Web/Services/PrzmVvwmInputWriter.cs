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
        // --- Chemical flags and counts ---
        WriteCsvLine(sw,
            project.ChemicalInputs.SorptionType == "Koc",
            project.AdvancedSettings.UseFreundlich,
            project.AdvancedSettings.UseNonequilibrium,
            project.PoundToKiloConversion,
            project.AdvancedSettings.IsHydrolysisOverride
        );

        WriteCsvLine(sw,
    project.ChemicalInputs.ChemicalLevel == "Parent" ? 1 :
    project.ChemicalInputs.ChemicalLevel == "Daughter" ? 2 :
    3
);

        // --- Chemical section (one chemical set for now) ---
        WriteCsvLine(sw, project.ChemicalInputs.SorptionParent, project.ChemicalInputs.SorptionDaughter, project.ChemicalInputs.SorptionGranddaughter);
        WriteCsvLine(sw, project.AdvancedSettings.N1Parent, project.AdvancedSettings.N1Daughter, project.AdvancedSettings.N1Granddaughter);
        WriteCsvLine(sw, project.AdvancedSettings.Kf2Parent, project.AdvancedSettings.Kf2Daughter, project.AdvancedSettings.Kf2Granddaughter);
        WriteCsvLine(sw, project.AdvancedSettings.N2Parent, project.AdvancedSettings.N2Daughter, project.AdvancedSettings.N2Granddaughter);
        WriteCsvLine(sw, project.AdvancedSettings.KsParent, project.AdvancedSettings.KsDaughter, project.AdvancedSettings.KsGranddaughter);
        WriteCsvLine(sw, project.AdvancedSettings.LowestConcentrationForFreundlichMgL, project.AdvancedSettings.SubTimeSteps);

        WriteCsvLine(sw, project.ChemicalInputs.WaterColumnHalflifeParent, project.ChemicalInputs.WaterColumnHalflifeDaughter, project.ChemicalInputs.WaterColumnHalflifeGranddaughter, project.ChemicalInputs.WaterColumnMolarRatioDaughter, project.ChemicalInputs.WaterColumnMolarRatioGranddaughter);
        WriteCsvLine(sw, project.ChemicalInputs.WaterReferenceTemperatureParent, project.ChemicalInputs.WaterReferenceTemperatureDaughter, project.ChemicalInputs.WaterReferenceTemperatureGranddaughter);
        WriteCsvLine(sw, project.ChemicalInputs.BenthicHalflifeParent, project.ChemicalInputs.BenthicHalflifeDaughter, project.ChemicalInputs.BenthicHalflifeGranddaughter, project.ChemicalInputs.BenthicMolarRatioDaughter, project.ChemicalInputs.BenthicMolarRatioGranddaughter);
        WriteCsvLine(sw, project.ChemicalInputs.BenthicReferenceTemperatureParent, project.ChemicalInputs.BenthicReferenceTemperatureDaughter, project.ChemicalInputs.BenthicReferenceTemperatureGranddaughter);
        WriteCsvLine(sw, project.ChemicalInputs.PhotoHalflifeParent, project.ChemicalInputs.PhotoHalflifeDaughter, project.ChemicalInputs.PhotoHalflifeGranddaughter, project.ChemicalInputs.PhotoMolarRatioDaughter, project.ChemicalInputs.PhotoMolarRatioGranddaughter);
        WriteCsvLine(sw, project.ChemicalInputs.PhotoReferenceLatitudeParent, project.ChemicalInputs.PhotoReferenceLatitudeDaughter, project.ChemicalInputs.PhotoReferenceLatitudeGranddaughter);
        WriteCsvLine(sw, project.ChemicalInputs.HydrolysisHalflifeParent, project.ChemicalInputs.HydrolysisHalflifeDaughter, project.ChemicalInputs.HydrolysisHalflifeGranddaughter, project.ChemicalInputs.HydrolysisMolarRatioDaughter, project.ChemicalInputs.HydrolysisMolarRatioGranddaughter);
  
        WriteCsvLine(sw, project.ChemicalInputs.SoilHalflifeParent, project.ChemicalInputs.SoilHalflifeDaughter, project.ChemicalInputs.SoilHalflifeGranddaughter, project.ChemicalInputs.SoilMolarRatioDaughter, project.ChemicalInputs.SoilMolarRatioGranddaughter, project.AdvancedSettings.IsHydrolysisOverride);
        WriteCsvLine(sw, project.ChemicalInputs.SoilReferenceTemperatureParent, project.ChemicalInputs.SoilReferenceTemperatureDaughter, project.ChemicalInputs.SoilReferenceTemperatureGranddaughter);
        WriteCsvLine(sw, project.ChemicalInputs.FoliarHalflifeParent, project.ChemicalInputs.FoliarHalflifeDaughter, project.ChemicalInputs.FoliarHalflifeGranddaughter, project.ChemicalInputs.FoliarMolarRatioDaughter, project.ChemicalInputs.FoliarMolarRatioGranddaughter);
        WriteCsvLine(sw, project.ChemicalInputs.FoliarWashoffParent, project.ChemicalInputs.FoliarWashoffDaughter, project.ChemicalInputs.FoliarWashoffGranddaughter);
        WriteCsvLine(sw, project.ChemicalInputs.MWTParent, project.ChemicalInputs.MWTDaughter, project.ChemicalInputs.MWTGranddaughter);
        WriteCsvLine(sw, project.ChemicalInputs.VaporPressureParent, project.ChemicalInputs.VaporPressureDaughter, project.ChemicalInputs.VaporPressureGranddaughter);
        WriteCsvLine(sw, project.ChemicalInputs.SolubilityParent, project.ChemicalInputs.SolubilityDaughter, project.ChemicalInputs.SolubilityGranddaughter);
        WriteCsvLine(sw, project.ChemicalInputs.HenryParent, project.ChemicalInputs.HenryDaughter, project.ChemicalInputs.HenryGranddaughter);
        WriteCsvLine(sw, project.ChemicalInputs.AirDiffusionParent, project.ChemicalInputs.AirDiffusionDaughter, project.ChemicalInputs.AirDiffusionGranddaughter);
        WriteCsvLine(sw, project.ChemicalInputs.HeatHenryParent, project.ChemicalInputs.HeatHenryDaughter, project.ChemicalInputs.HeatHenryGranddaughter);
        WriteCsvLine(sw, project.AdvancedSettings.Q10);
        WriteCsvLine(sw, project.AdvancedSettings.SubsurfaceDegradationProfile == "Constant");
        WriteCsvLine(sw, project.AdvancedSettings.SubsurfaceDegradationProfile == "Ramp", project.AdvancedSettings.RampFirstPlateauCm, project.AdvancedSettings.RampSecondPlateauCm, project.AdvancedSettings.RampSecondPlateauValueFraction);
        WriteCsvLine(sw, project.AdvancedSettings.SubsurfaceDegradationProfile == "Exponential", project.AdvancedSettings.ExponentialExponent, project.AdvancedSettings.ExponentialAsymptoteFraction);
        WriteCsvLine(sw, project.Schemes.Count);

        for (int i = 0; i < project.Schemes.Count; i++)
        {
            var scheme = project.Schemes[i];

            WriteCsvLine(sw, i + 1, $"\"{scheme.Description}\"");

            int appReferencePoint = scheme.Details.ApplicationMode == "Absolute Days" ? 0 :
                                    scheme.Details.ApplicationMode == "Emerge" ? 1 :
                                    scheme.Details.ApplicationMode == "Maturity" ? 2 :
                                    scheme.Details.ApplicationMode == "Removal" ? 3 : 0;
            WriteCsvLine(sw, appReferencePoint);
            WriteCsvLine(sw, scheme.Details.Rows.Count);

            foreach (var row in scheme.Details.Rows)
            {
                WriteCsvLine(sw, row.Day, row.Amount, row.Method, row.Depth, row.Split, row.Drift, row.DriftBuffer, row.Periodicity, row.Lag);
            }
            WriteCsvLine(sw, scheme.Details.UseApplicationWindow, scheme.Details.ApplicationWindowSpan, scheme.Details.ApplicationWindowStep);
            WriteCsvLine(sw, scheme.Details.UseRainFast, scheme.Details.RainLimit, scheme.Details.IntolerableRainWindow, scheme.Details.OptimumApplicationWindow, scheme.Details.MinDaysBetweenApps);
            WriteCsvLine(sw, scheme.Details.Scenarios.Count);
            foreach (var scenario in scheme.Details.Scenarios)
            {
                WriteCsvLineNoTrailComma(sw, scenario);
            }
            WriteCsvLine(sw, scheme.Details.UseBatchScenarioFile);
            WriteCsvLineNoTrailComma(sw, scheme.Details.ScenarioBatchFileName);
            WriteCsvLine(sw, "Mitigations (flag to make older versions still readable)");
            WriteCsvLine(sw, scheme.Details.RunoffMitigation, scheme.Details.ErosionMitigation, scheme.Details.DriftMitigation);
        }
        WriteCsvLine(sw, project.AdvancedSettings.ErosionModel);
        sw.WriteLine();
        sw.WriteLine();
        sw.WriteLine();
        sw.WriteLine();
        sw.WriteLine();
        WriteCsvLine(sw, project.AdvancedSettings.AdjustCnForSoilMoisture);
        WriteCsvLine(sw, project.Waterbody.UseEpaPond, project.Waterbody.UseEpaReservoir, project.Waterbody.SpecialWaterbodies.Count > 0, project.Waterbody.UseEpaTpezWpez, project.Waterbody.UseSprayBuffersForTpez);

        WriteCsvLine(sw, project.Waterbody.SpecialWaterbodies.Count);

        foreach (var wb in project.Waterbody.SpecialWaterbodies)
        {
            WriteCsvLine(sw, wb);
        }
        WriteCsvLine(sw, project.OutputOptions.OutputRunoff);
        WriteCsvLine(sw, project.OutputOptions.OutputErosion);
        WriteCsvLine(sw, project.OutputOptions.OutputPestRunoff);
        WriteCsvLine(sw, project.OutputOptions.OutputPestErosion);
        WriteCsvLine(sw, project.OutputOptions.OutputConcLastLayer);
        WriteCsvLine(sw, project.OutputOptions.OutputDailyFieldVolatilization);
        WriteCsvLine(sw, project.OutputOptions.OutputDailyPestLeached, project.OutputOptions.ChemInfiltrationDepth);
        WriteCsvLine(sw, project.OutputOptions.OutputDecayedPest, project.OutputOptions.OutputDecayDepth1, project.OutputOptions.OutputDecayDepth2);
        WriteCsvLine(sw, project.OutputOptions.OutputMassInSoilProfile);
        WriteCsvLine(sw, project.OutputOptions.OutputMassSoilSpecific, project.OutputOptions.OutputMassDepth1, project.OutputOptions.OutputMassDepth2);
        WriteCsvLine(sw, project.OutputOptions.OutputMassOnFoliage);
        WriteCsvLine(sw, project.OutputOptions.OutputPrecipitation);
        WriteCsvLine(sw, project.OutputOptions.OutputActualEvap);
        WriteCsvLine(sw, project.OutputOptions.OutputTotalSoilWater);
        WriteCsvLine(sw, project.OutputOptions.OutputIrrigation);
        WriteCsvLine(sw, project.OutputOptions.OutputInfiltrationAtDepth, project.OutputOptions.OutputInfiltrationDepth);
        WriteCsvLine(sw, project.OutputOptions.OutputInfiltratedWaterLastLayer);
        WriteCsvLine(sw, project.OutputOptions.OutputWaterConc);
        WriteCsvLine(sw, project.OutputOptions.OutputSpraydrift);
        WriteCsvLine(sw, project.OutputOptions.OutputGwBtc);
        sw.WriteLine("holder for future expansion,");
        sw.WriteLine("holder for future expansion,");
        sw.WriteLine("holder for future expansion,");
        sw.WriteLine("holder for future expansion,");
        WriteCsvLine(sw, project.OutputOptions.CalculateEoF);
        WriteCsvLine(sw, project.OutputOptions.AdditionalOutputRows.Count);
        foreach (var row in project.OutputOptions.AdditionalOutputRows)
        {
            WriteCsvLine(sw, row.Item, row.Chem, row.Mode, row.Arg1, row.Arg2, row.Multiplier);
        }

    }
}