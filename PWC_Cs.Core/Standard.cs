
namespace PWC_Cs.Core
{
    public static class Standard
    {
        internal const string VersionNumber = "4.0";

        public const string Method1 = "Below Crop";
        public const string Method2 = "Above Crop";
        public const string Method3 = "Uniform";
        public const string Method4 = "@ a Depth";
        public const string Method5 = "T-Band";
        public const string Method6 = "△";
        public const string Method7 = "▽";

        public static readonly string[] SprayTerms = new[]
        {
            "dummy to take up zero spot",
            "Aerial Fine (50% Boom)",
            "Aerial Medium, EPA Default",
            "Aerial Course",
            "Aerial Very Course",
            "Ground High Boom, VF-F, EPA Default",
            "Ground High Boom, F-MC",
            "Ground Low Boom, VF-F",
            "Ground Low Boom, F-MC",
            "Airblast Normal",
            "Airblast Dense",
            "Airblast Sparse, EPA Default",
            "Airblast Vinyard",
            "Airblast Orchard",
            "Spray Factor = 1",
            "None"
        };




    }
}

