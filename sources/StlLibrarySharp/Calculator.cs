using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StlLibrarySharp
{

    /// <summary>
    /// Helpers for calculation
    /// </summary>
    public static class Calculator
    {

        /// <summary>
        /// Calculate the signed volume for a triangle facet
        /// </summary>
        /// <remarks>
        /// Code base on https://github.com/mcanet/STL-Volume-Model-Calculator
        /// </remarks>
        public static float CalculateSignedVolume(Facet facet)
        {
            if (facet == null) return 0;

            var p1 = facet.Vertices[0];
            var p2 = facet.Vertices[1];
            var p3 = facet.Vertices[2];

            var v321 = p3.X * p2.Y * p1.Z;
            var v231 = p2.X * p3.Y * p1.Z;
            var v312 = p3.X * p1.Y * p2.Z;
            var v132 = p1.X * p3.Y * p2.Z;
            var v213 = p2.X * p1.Y * p3.Z;
            var v123 = p1.X * p2.Y * p3.Z;
            return (1.0f / 6.0f) * (-v321 + v231 + v312 - v132 - v213 + v123);
        }

    }

}
