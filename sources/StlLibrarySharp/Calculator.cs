using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace STL
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
        /// Code based on https://github.com/mcanet/STL-Volume-Model-Calculator
        /// </remarks>
        public static float CalculateSignedVolume(Facet facet)
        {
            if (facet == null) return 0;

            var p1 = facet.Vertices[0];
            var p2 = facet.Vertices[1];
            var p3 = facet.Vertices[2];

            var v321 = (double)p3.X * (double)p2.Y * (double)p1.Z;
            var v231 = (double)p2.X * (double)p3.Y * (double)p1.Z;
            var v312 = (double)p3.X * (double)p1.Y * (double)p2.Z;
            var v132 = (double)p1.X * (double)p3.Y * (double)p2.Z;
            var v213 = (double)p2.X * (double)p1.Y * (double)p3.Z;
            var v123 = (double)p1.X * (double)p2.Y * (double)p3.Z;
            return (float)((1.0 / 6.0) * (-v321 + v231 + v312 - v132 - v213 + v123));
        }

        /// <summary>
        /// Calculate the volume for a list of facets
        /// </summary>
        /// <remarks>
        /// Code based on https://github.com/admesh/admesh/
        /// </remarks>
        public static float CalculateVolume(IEnumerable<Facet> facets)
        {
            float result = 0;

            if (facets == null) return result;

            var enumerator = facets.GetEnumerator();
            try
            {
                if (!enumerator.MoveNext()) return result;
                Facet facet = enumerator.Current;
                Vertex p0 = new Vertex(facet.Vertices[0]);
                do
                {
                    facet = enumerator.Current;
                    var vertice = facet.Vertices[0];
                    Vertex p = new Vertex(
                        vertice.X - p0.X,
                        vertice.Y - p0.Y,
                        vertice.Z - p0.Z
                    );
                    var n = facet.Normal;
                    var height = (n.X * p.X) + (n.Y * p.Y) + (n.Z * p.Z);
                    var area = CalculateArea(facet);
                    result += (float)((area * height) / 3.0);
                }
                while (enumerator.MoveNext());
            }
            finally
            {
                if (enumerator is IDisposable)
                    ((IDisposable)enumerator).Dispose();
            }
            return result;
        }

        /// <summary>
        /// Calculate the size of a list of facets
        /// </summary>
        /// <remarks>
        /// Code based on https://github.com/admesh/admesh/
        /// </remarks>
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

        /// <summary>
        /// Calcul the normal vertex
        /// </summary>
        /// <remarks>
        /// Code based on https://github.com/admesh/admesh/
        /// </remarks>
        public static Vertex CalculateNormal(Facet facet)
        {
            if (facet == null) return null;

            float[] v1 = new float[3];
            float[] v2 = new float[3];

            v1[0] = facet.Vertices[1].X - facet.Vertices[0].X;
            v1[1] = facet.Vertices[1].Y - facet.Vertices[0].Y;
            v1[2] = facet.Vertices[1].Z - facet.Vertices[0].Z;
            v2[0] = facet.Vertices[2].X - facet.Vertices[0].X;
            v2[1] = facet.Vertices[2].Y - facet.Vertices[0].Y;
            v2[2] = facet.Vertices[2].Z - facet.Vertices[0].Z;

            return new Vertex(
                (float)(((double)v1[1] * (double)v2[2]) - ((double)v1[2] * (double)v2[1])),
                (float)(((double)v1[2] * (double)v2[0]) - ((double)v1[0] * (double)v2[2])),
                (float)(((double)v1[0] * (double)v2[1]) - ((double)v1[1] * (double)v2[0]))
            );
        }

        /// <summary>
        /// Normalize a vertex
        /// </summary>
        /// <remarks>
        /// Code based on https://github.com/admesh/admesh/
        /// </remarks>
        public static void NormalizeVertex(Vertex v)
        {
            if (v == null) return;
            double length = Math.Sqrt((double)v.X * (double)v.X + (double)v.Y * (double)v.Y + (double)v.Z * (double)v.Z);
            float min_normal_length = 0.000000000001f;
            if (length < min_normal_length)
            {
                v.X = 0.0f;
                v.Y = 0.0f;
                v.Z = 0.0f;
                return;
            }
            var factor = 1.0 / length;
            v.X *= (float)factor;
            v.Y *= (float)factor;
            v.Z *= (float)factor;
        }

        /// <summary>
        /// Calculate the area for a facet
        /// </summary>
        /// <remarks>
        /// Code based on https://github.com/admesh/admesh/
        /// </remarks>
        public static float CalculateArea(Facet facet)
        {
            if (facet == null) return 0;

            double[,] cross = new double[3, 3];
            for (int i = 0; i < 3; i++)
            {
                var v1 = facet.Vertices[i];
                var v2 = facet.Vertices[(i + 1) % 3];
                cross[i, 0] = (((double)v1.Y * (double)v2.Z) - ((double)v1.Z * (double)v2.Y));
                cross[i, 1] = (((double)v1.Z * (double)v2.X) - ((double)v1.X * (double)v2.Z));
                cross[i, 2] = (((double)v1.X * (double)v2.Y) - ((double)v1.Y * (double)v2.X));
            }

            double[] sum = new double[]
            {
                cross[0,0] + cross[1,0] + cross[2,0],
                cross[0,1] + cross[1,1] + cross[2,1],
                cross[0,2] + cross[1,2] + cross[2,2],
            };

            var n = CalculateNormal(facet);
            NormalizeVertex(n);

            return (float)(0.5 * (n.X * sum[0] + n.Y * sum[1] + n.Z * sum[2]));
        }

        /// <summary>
        /// Calculate the surface area of a list à facets
        /// </summary>
        /// <remarks>
        /// Code based on https://github.com/admesh/admesh/
        /// </remarks>
        public static float CalculateSurfaceArea(IEnumerable<Facet> facets)
        {
            if (facets == null) return 0;
            return facets.Sum(f => CalculateArea(f));
        }

    }

}
