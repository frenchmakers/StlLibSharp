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

        /// <summary>
        /// Calculate the size of a list of facets
        /// </summary>
        public static SolidSize CalculateSize(IEnumerable<Facet> facets)
        {
            SolidSize result = new SolidSize();

            if (facets == null) return result;

            var enumerator = facets.SelectMany(f => f.Vertices).GetEnumerator();
            try
            {
                if (!enumerator.MoveNext()) return result;
                Vertex min = new Vertex(enumerator.Current);
                Vertex max = new Vertex(enumerator.Current);
                while (enumerator.MoveNext())
                {
                    var vertice = enumerator.Current;
                    min.X = Math.Min(min.X, vertice.X);
                    min.Y = Math.Min(min.Y, vertice.Y);
                    min.Z = Math.Min(min.Z, vertice.Z);
                    max.X = Math.Max(max.X, vertice.X);
                    max.Y = Math.Max(max.Y, vertice.Y);
                    max.Z = Math.Max(max.Z, vertice.Z);
                }
                result.Min = min;
                result.Max = max;
                result.Size = new Vertex(max.X - min.X, max.Y - min.Y, max.Z - min.Z);
                result.BoudingDiameter = Math.Sqrt(
                    result.Size.X * result.Size.X
                    + result.Size.Y * result.Size.Y
                    + result.Size.Z * result.Size.Z
                    );
            }
            finally
            {
                if (enumerator is IDisposable)
                    ((IDisposable)enumerator).Dispose();
            }
            return result;
        }

    }

}
