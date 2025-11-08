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
        public IList<string> Days { get; set; }
        public IList<string> Amount { get; set; }
        public IList<string> Method { get; set; }
        public IList<string> Depth { get; set; }
        public IList<string> Split { get; set; }
        public List<string> Drift { get; set; }
        public List<string> DriftBuffer { get; set; }
        public IList<string> Periodicity { get; set; }
        public IList<string> Lag { get; set; }
        public IList<string> Scenarios { get; set; }
        public bool AbsoluteRelative { get; set; }
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
        public string RunoffMitigation { get; set; }
        public string ErosionMitigation { get; set; }
        public string DriftMitigation { get; set; }
    }
}

