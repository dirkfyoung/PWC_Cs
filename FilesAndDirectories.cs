using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PWC_Cs
{

    public static class FileNames
    {
        // sensible defaults
        private static readonly string DefaultDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        // Core folders/paths
        public static string WorkingDirectory { get; set; } = string.Empty; // = DefaultDocuments;

        // Weather & input files
        public static string WeatherFileDirectory { get; set; } = string.Empty;

        // Scheme / defaults
        public static string SchemeFileName { get; set; } = string.Empty;
        public static string WaterBodyDirectory { get; set; } = DefaultDocuments;
        public static string ScenarioDirectory { get; set; } = DefaultDocuments;


    }


}
