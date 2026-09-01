using PWC_Cs.Core.Models;

namespace PWC_Cs.Web.Services;

public class ProjectState
{
    public ProjectModel Current { get; private set; } = new()
    {
        ProjectName = "Test Project",
        RunName = "Test Run",
        Schemes = new List<SchemeModel>
        {
            new SchemeModel
            {
                Name = "Scheme 1",
                Description = "Starter scheme"
            }
        }
    };

    public SchemeModel? SelectedScheme { get; private set; }

    public void Reset()
    {
        Current = new ProjectModel();
        SelectedScheme = null;
    }

    public void Load(ProjectModel project)
    {
        Current = project;
        SelectedScheme = null;
    }

    public void AddScheme()
    {
        Current.Schemes.Add(new SchemeModel
        {
            Name = $"Scheme {Current.Schemes.Count + 1}",
            Description = string.Empty
        });
    }

    public void SelectScheme(SchemeModel scheme)
    {
        SelectedScheme = scheme;
    }
}