using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PWC_Cs
{
    internal static class Standard
    {
        internal const string Method1 = "Below Crop";
        internal const string Method2 = "Above Crop";
        internal const string Method3 = "Uniform";
        internal const string Method4 = "@ a Depth";
        internal const string Method5 = "T-Band";
        internal const string Method6 = "△";
        internal const string Method7 = "▽";

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
