using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StlLibrarySharp
{
    /// <summary>
    /// Extension methods
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Get the signed volume of a facet
        /// </summary>
        public static float GetSignedVolume(this Facet facet)
        {
            return Calculator.CalculateSignedVolume(facet);
        }

        /// <summary>
        /// Get signed volume of a list of facets
        /// </summary>
        public static float GetSignedVolume(this IEnumerable<Facet> facets)
        {
            return facets.Sum(f => f.GetSignedVolume());
        }

        /// <summary>
        /// Get signed volume of a solid
        /// </summary>
        public static float GetSignedVolume(this Solid solid)
        {
            return solid.Facets.GetSignedVolume();
        }

    }
}
