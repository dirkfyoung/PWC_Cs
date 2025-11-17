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
        public static string WorkingDirectory { get; set; } //= DefaultDocuments;
        public static string PreviousScenarioPath { get; set; } = string.Empty;
        public static string PreviousWaterBodyPath { get; set; } = string.Empty;

        // Weather & input files
        public static string WeatherFile { get; set; } = string.Empty;
        public static string WeatherFileDirectory { get; set; } = string.Empty;
        public static string PreviousWeatherPath { get; set; } = string.Empty;
        public static string ErrorFileName { get; set; } = string.Empty;
        public static string InputFileName { get; set; } = string.Empty;

        // Scheme / defaults
        public static string SchemeFileName { get; set; } = string.Empty;
        public static string DefaultWaterBodyDirectory { get; set; } = DefaultDocuments;
        public static string DefaultScenarioDirectory { get; set; } = DefaultDocuments;


        //// batch / output names
        //public static string BatchOutputVVWM { get; set; } = string.Empty;

        //public static string VVWMoutputFileParent { get; set; } = string.Empty;
        //public static string VVWMoutputFileDeg1 { get; set; } = string.Empty;
        //public static string VVWMoutputFileDeg2 { get; set; } = string.Empty;

        //public static string VVWMoutputFileParentESA { get; set; } = string.Empty;
        //public static string VVWMoutputFileDeg1ESA { get; set; } = string.Empty;
        //public static string VVWMoutputFileDeg2ESA { get; set; } = string.Empty;

        //public static string VVWMoutputFileParentTS { get; set; } = string.Empty;
        //public static string VVWMoutputFileDeg1TS { get; set; } = string.Empty;
        //public static string VVWMoutputFileDeg2TS { get; set; } = string.Empty;

        //public static string PondParentFile { get; set; } = string.Empty;
        //public static string PondDeg1File { get; set; } = string.Empty;
        //public static string PondDeg2File { get; set; } = string.Empty;

        //public static string ReservoirParentFile { get; set; } = string.Empty;
        //public static string ReservoirDeg1File { get; set; } = string.Empty;
        //public static string ReservoirDeg2File { get; set; } = string.Empty;

        //public static string CustomParentFile { get; set; } = string.Empty;
        //public static string CustomDeg1File { get; set; } = string.Empty;
        //public static string CustomDeg2File { get; set; } = string.Empty;

        //// scenario/run identifiers and others
        //public static string ScenarioRunID { get; set; } = string.Empty;
        //public static string PWCNewScenariosFilename { get; set; } = string.Empty;

        // Helpers

        ///// <summary>Return a validated folder path or null if it doesn't exist.</summary>
        //public static string? GetValidatedFolder(string? folder) =>
        //    string.IsNullOrWhiteSpace(folder) || !Directory.Exists(folder) ? null : folder;

        ///// <summary>Initialize defaults from existing values or fallbacks (call at app startup).</summary>
        //public static void InitializeDefaults()
        //{
        //    WorkingDirectory = GetValidatedFolder(WorkingDirectory) ?? DefaultDocuments;
        //    DefaultWaterBodyDirectory = GetValidatedFolder(DefaultWaterBodyDirectory) ?? DefaultDocuments;
        //    DefaultScenarioDirectory = GetValidatedFolder(DefaultScenarioDirectory) ?? DefaultDocuments;
        //}

        ///// <summary>Optional: call this before showing an OpenFileDialog to pick a reasonable InitialDirectory.</summary>
        //public static string InitialDirectoryForWaterBody()
        //{
        //    return GetValidatedFolder(PreviousWaterBodyPath)
        //           ?? GetValidatedFolder(WeatherFileDirectory)
        //           ?? GetValidatedFolder(DefaultWaterBodyDirectory)
        //           ?? WorkingDirectory;
        //}

        ///// <summary>Optional: ensure parent directory exists for a file path. Returns true if OK.</summary>
        //public static bool EnsureParentDirectoryExists(string? filePath)
        //{
        //    if (string.IsNullOrWhiteSpace(filePath)) return false;
        //    var dir = Path.GetDirectoryName(filePath);
        //    if (string.IsNullOrEmpty(dir)) return false;
        //    Directory.CreateDirectory(dir);
        //    return true;
        //}
    }


}
