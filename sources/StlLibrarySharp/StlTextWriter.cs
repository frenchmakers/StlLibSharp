using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace StlLibrarySharp
{
    /// <summary>
    /// A text file format writer
    /// </summary>
    public class StlTextWriter : StlWriter
    {
        /// <summary>
        /// Create a new text writer
        /// </summary>
        public StlTextWriter(Stream stream, bool owns = false) 
            : base(stream, owns)
        {
        }

        /// <summary>
        /// Write a facet
        /// </summary>
        protected virtual void WriteFacet(Facet facet, TextWriter writer)
        {
            // Write the normal
            writer.WriteLine("\tfacet normal {0}", VertexToString(facet.Normal));

            // Write each vertice
            writer.WriteLine("\t\touter loop");
            foreach (var vertice in facet.Vertices)
            {
                writer.WriteLine("\t\t\tvertex {0}", VertexToString(vertice));
            }

            // Write the end loop
            writer.WriteLine("\t\tendloop");
            writer.WriteLine("\tendfacet");
        }

        /// <summary>
        /// Convert a vertex to string
        /// </summary>
        protected virtual String VertexToString(Vertex vertex)
        {
            return String.Format(CultureInfo.InvariantCulture, "{0} {1} {2}", vertex.X, vertex.Y, vertex.Z);
        }

        /// <summary>
        /// Internal write a solid
        /// </summary>
        protected override void InternalWriteSolid(Solid solid)
        {
            using(var writer=new StreamWriter(BaseStream, Consts.FileEncoding))
            {
                // Write header
                writer.WriteLine("solid {0}", solid.Name);

                // Foreach facet
                foreach (var facet in solid.Facets)
                    WriteFacet(facet, writer);

                // Write footer
                writer.WriteLine("endsolid {0}", solid.Name);
            }
        }

    }
}
