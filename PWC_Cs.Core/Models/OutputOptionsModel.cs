namespace PWC_Cs.Core.Models;

public class OutputOptionsModel
{
    public bool IncludeSummaryOutput { get; set; }
    public bool IncludeDetailedOutput { get; set; }
    public bool IncludeGraphs { get; set; }
    public string? OutputDirectory { get; set; }
}
