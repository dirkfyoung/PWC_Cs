namespace PWC_Cs.Core.Models;

public class ProjectModel
{
    public string? ProjectName { get; set; }
    public string? RunName { get; set; }

    public ChemicalInputsModel ChemicalInputs { get; set; } = new();
    public List<SchemeModel> Schemes { get; set; } = new();
    public OutputOptionsModel OutputOptions { get; set; } = new();
    public AdvancedSettingsModel AdvancedSettings { get; set; } = new();
}