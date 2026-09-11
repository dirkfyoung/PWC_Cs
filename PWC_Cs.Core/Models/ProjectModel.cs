namespace PWC_Cs.Core.Models;

public class ProjectModel
{
    public string? WorkingDirectory { get; set; }
    public string? FamilyName { get; set; }
    public string? WeatherFileDirectory { get; set; }

   public bool PoundToKiloConversion { get; set; }
    public ChemicalInputsModel ChemicalInputs { get; set; } = new();
    public List<SchemeModel> Schemes { get; set; } = new();
    public OutputOptionsModel OutputOptions { get; set; } = new();
    public AdvancedSettingsModel AdvancedSettings { get; set; } = new();
    public WaterbodyModel Waterbody { get; set; } = new();
}