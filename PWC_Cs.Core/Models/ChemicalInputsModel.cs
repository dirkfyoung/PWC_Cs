using System.ComponentModel.DataAnnotations;

namespace PWC_Cs.Core.Models;

public class ChemicalInputsModel
{
    [Required(ErrorMessage = "Sorption is required.")]
    public string Sorption { get; set; } = string.Empty;

    [Required(ErrorMessage = "Water Column Halflife is required.")]
    public string WaterColumnHalflife { get; set; } = string.Empty;

    [Required(ErrorMessage = "Water Reference Temperature is required.")]
    public string WaterReferenceTemperature { get; set; } = string.Empty;

    [Required(ErrorMessage = "Benthic Halflife is required.")]
    public string BenthicHalflife { get; set; } = string.Empty;

    [Required(ErrorMessage = "Benthic Reference Temperature is required.")]
    public string BenthicReferenceTemperature { get; set; } = string.Empty;

    [Required(ErrorMessage = "Photo Halflife is required.")]
    public string PhotoHalflife { get; set; } = string.Empty;

    [Required(ErrorMessage = "Photo Reference Temperature is required.")]
    public string PhotoReferenceTemperature { get; set; } = string.Empty;

    [Required(ErrorMessage = "Hydrolysis Halflife is required.")]
    public string HydrolysisHalflife { get; set; } = string.Empty;

    [Required(ErrorMessage = "Hydrolysis Reference Temperature is required.")]
    public string HydrolysisReferenceTemperature { get; set; } = string.Empty;

    [Required(ErrorMessage = "Soil Halflife is required.")]
    public string SoilHalflife { get; set; } = string.Empty;

    [Required(ErrorMessage = "Soil Reference Temperature is required.")]
    public string SoilReferenceTemperature { get; set; } = string.Empty;

    [Required(ErrorMessage = "Foliar Halflife is required.")]
    public string FoliarHalflife { get; set; } = string.Empty;

    [Required(ErrorMessage = "Foliar Washoff is required.")]
    public string FoliarWashoff { get; set; } = string.Empty;

    [Required(ErrorMessage = "MWT is required.")]
    public string MWT { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vapor Pressure is required.")]
    public string VaporPressure { get; set; } = string.Empty;

    [Required(ErrorMessage = "Solubility is required.")]
    public string Solubility { get; set; } = string.Empty;

    [Required(ErrorMessage = "Henry is required.")]
    public string Henry { get; set; } = string.Empty;

    [Required(ErrorMessage = "Air Diffusion is required.")]
    public string AirDiffusion { get; set; } = string.Empty;

    [Required(ErrorMessage = "Heat Henry is required.")]
    public string HeatHenry { get; set; } = string.Empty;

    [Required(ErrorMessage = "Q10 is required.")]


    public string? SorptionType { get; set; }
    public bool PoundToKiloConversion { get; set; }


    

}