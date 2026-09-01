using System.Collections.Generic;

namespace PWC_Cs.Core.Models;

public class SchemeDetails
{
    public string? SchemeDescription { get; set; }

    public List<SchemeTableRowModel> Rows { get; set; } = new();

    public bool AbsoluteDays { get; set; }
    public bool Emerge { get; set; }
    public bool Maturity { get; set; }
    public bool Removal { get; set; }

    public bool UseApplicationWindow { get; set; }
    public string? ApplicationWindowSpan { get; set; }
    public string? ApplicationWindowStep { get; set; }

    public bool UseRainFast { get; set; }
    public string? RainLimit { get; set; }
    public string? IntolerableRainWindow { get; set; }
    public string? OptimumApplicationWindow { get; set; }
    public string? MinDaysBetweenApps { get; set; }

    public List<string> Scenarios { get; set; } = new();
    public bool UseBatchScenarioFile { get; set; }
    public string? ScenarioBatchFileName { get; set; }

    public string? RunoffMitigation { get; set; } = "1.0";
    public string? ErosionMitigation { get; set; } = "1.0";
    public string? DriftMitigation { get; set; } = "1.0";
}

