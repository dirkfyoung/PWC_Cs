using System.Text.Json;
using PWC_Cs.Core.Models;

namespace PWC_Cs.Web.Services;

public class ProjectFileService
{
    private readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true
    };

    public async Task SaveAsync(ProjectModel project, string filePath)
    {
        var folder = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrWhiteSpace(folder) && !Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }

        var json = JsonSerializer.Serialize(project, _options);
        await File.WriteAllTextAsync(filePath, json);
    }

    public async Task<ProjectModel> LoadAsync(string filePath)
    {
        var json = await File.ReadAllTextAsync(filePath);
        return JsonSerializer.Deserialize<ProjectModel>(json, _options) ?? new ProjectModel();
    }
}