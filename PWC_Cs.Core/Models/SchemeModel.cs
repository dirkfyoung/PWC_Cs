namespace PWC_Cs.Core.Models;

public class SchemeModel
{
    public string? Name { get; set; }
    public string? Description { get; set; }

    public SchemeDetails Details { get; set; } = new();
}
