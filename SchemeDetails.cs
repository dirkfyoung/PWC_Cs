using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PWC_Cs
{
    internal class SchemeDetails
    {
        public string SchemeDescription { get; set; } 
        public List<string> Days { get; set; } = new List<string>();
        public List<string> Amount { get; set; } = new List<string>();
        public List<string> Method { get; set; } = new List<string>();
        public List<string> Depth { get; set; } = new List<string>();
        public List<string> Split { get; set; } = new List<string>();
        public List<string> Drift { get; set; } = new List<string>();
        public List<string> DriftBuffer { get; set; } = new List<string>();
        public List<string> Periodicity { get; set; } = new List<string>();
        public List<string> Lag { get; set; } = new List<string>();
        public List<string> Scenarios { get; set; } = new List<string>();
        public bool AbsoluteDays { get; set; } 
        public bool Emerge { get; set; }
        public bool Maturity { get; set; }
        public bool Removal { get; set; }
        public bool UseApplicationWindow { get; set; }
        public string ApplicationWindowSpan { get; set; } 
        public string ApplicationWindowStep { get; set; }
        public bool UseRainFast { get; set; }
        public string RainLimit { get; set; }
        public string IntolerableRainWindow { get; set; }
        public string OptimumApplicationWindow { get; set; }
        public string MinDaysBetweenApps { get; set; }
        public bool UseBatchScenarioFile { get; set; }
        public string ScenarioBatchFileName { get; set; }
        public string RunoffMitigation { get; set; } = "1.0";
        public string ErosionMitigation { get; set; } = "1.0";
        public string DriftMitigation { get; set; } = "1.0";
    }
}

