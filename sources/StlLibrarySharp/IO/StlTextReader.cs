using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace STL
{
    /// <summary>
    /// A text file format reader
    /// </summary>
    public class StlTextReader : IStlReader
    {
        const String headerRegex = @"solid\s+(?<name>[^\r\n]+)?";
        const String facetRegex = @"\s*(facet normal)\s+(?<X>[^\s]+)\s+(?<Y>[^\s]+)\s+(?<Z>[^\s]+)";
        const String verticeRegex = @"\s*(vertex)\s+(?<X>[^\s]+)\s+(?<Y>[^\s]+)\s+(?<Z>[^\s]+)";

        /// <summary>
        /// Create a new text file reader
        /// </summary>
        public StlTextReader(Stream stream)
        {
            if (stream == null) throw new ArgumentNullException("stream");
            this.BaseStream = stream;
        }

        /// <summary>
        /// Read the header
        /// </summary>
        protected virtual Solid ReadHeader(TextReader reader)
        {
            // Read the header
            var header = reader.ReadLine();
            var match = Regex.Match(header, headerRegex, RegexOptions.IgnoreCase);
            if (!match.Success)
                throw new FormatException(String.Format("Invalid text STL file header. Expected 'solid [name]' but '{0}' found.", header));

            // Create the solid
            return new Solid(match.Groups["name"].Value);
        }

        /// <summary>
        /// Read a facet
        /// </summary>
        protected virtual Facet ReadFacet(TextReader reader)
        {
            Facet facet = new Facet();

            // Read the normal
            if ((facet.Normal = ReadVertex(reader, facetRegex)) == null)
                return null;

            // Skip the "outer loop"
            reader.ReadLine();

            // Read the vertices
            for (int i = 0; i < 3; i++)
            {
                var vertice = ReadVertex(reader, verticeRegex);
                if (vertice == null) return null;
                facet.Vertices.Add(vertice);
            }

            // Read the "endloop" and "endfacet"
            reader.ReadLine();
            reader.ReadLine();

            return facet;
        }

        /// <summary>
        /// Read a vertex
        /// </summary>
        protected virtual Vertex ReadVertex(TextReader reader, String regex)
        {
            // Read the next line of data
            String data = reader.ReadLine();
            if (String.IsNullOrWhiteSpace(data)) return null;

            // Parse the line
            var match = Regex.Match(data, regex, RegexOptions.IgnoreCase);
            if (!match.Success)
                return null;

            //Parse the three coordinates.
            float x, y, z;
            const NumberStyles numberStyle = (NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign);
            if (!float.TryParse(match.Groups["X"].Value, numberStyle, CultureInfo.InvariantCulture, out x))
                throw new FormatException(String.Format("Could not parse X coordinate '{0}' as a decimal.", match.Groups["X"]));

            if (!float.TryParse(match.Groups["Y"].Value, numberStyle, CultureInfo.InvariantCulture, out y))
                throw new FormatException(String.Format("Could not parse Y coordinate '{0}' as a decimal.", match.Groups["Y"]));

            if (!float.TryParse(match.Groups["Z"].Value, numberStyle, CultureInfo.InvariantCulture, out z))
                throw new FormatException(String.Format("Could not parse Z coordinate '{0}' as a decimal.", match.Groups["Z"]));

            // Returns the vertex
            return new Vertex(x, y, z);
        }

        /// <summary>
        /// Read a solid
        /// </summary>
        public virtual Solid ReadSolid()
        {
            using (var reader = new StreamReader(BaseStream, Consts.FileEncoding))
            {
                // Read the header
                Solid result = ReadHeader(reader);

                // Read the facets
                Facet facet;
                while ((facet = ReadFacet(reader)) != null)
                    result.Facets.Add(facet);

                return result;
            }
        }

        /// <summary>
        /// Extract all facets from file
        /// </summary>
        public virtual IEnumerable<Facet> ReadFacets()
        {
            using (var reader = new StreamReader(BaseStream, Consts.FileEncoding))
            {
                // Read the header
                Solid result = ReadHeader(reader);

                // Read the facets
                Facet facet;
                while ((facet = ReadFacet(reader)) != null)
                    yield return facet;
            }
        }

        /// <summary>
        /// Stream
        /// </summary>
        public Stream BaseStream { get; private set; }
    }

}