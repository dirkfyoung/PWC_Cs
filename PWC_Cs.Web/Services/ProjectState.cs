using PWC_Cs.Core.Models;

namespace PWC_Cs.Web.Services;

public class ProjectState
{
    public event Action? OnChange;

    public ProjectModel Current { get; private set; } = new()
    {
        ProjectName = "Test Project",
        RunName = "Test Run",
        Schemes = new List<SchemeModel>()
    };

    public SchemeModel? SelectedScheme { get; private set; }

    public void Reset()
    {
        Current = new ProjectModel
        {
            ProjectName = string.Empty,
            RunName = string.Empty,
            Schemes = new List<SchemeModel>()
        };
        SelectedScheme = null;
        NotifyStateChanged();
    }

    public void Load(ProjectModel project)
    {
        Current = project;
        SelectedScheme = null;
        NotifyStateChanged();
    }

    public void AddScheme()
    {
        Current.Schemes.Add(new SchemeModel
        {
            Name = $"Scheme {Current.Schemes.Count + 1}",
            Description = string.Empty
        });
        NotifyStateChanged();
    }

    public void SelectScheme(SchemeModel scheme)
    {
        SelectedScheme = scheme;
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}