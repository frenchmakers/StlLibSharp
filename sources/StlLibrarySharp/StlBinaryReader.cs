using System;
using System.IO;

namespace StlLibrarySharp
{

    /// <summary>
    /// A binary file format reader
    /// </summary>
    public class StlBinaryReader : IStlReader
    {
        /// <summary>
        /// Create a new text file reader
        /// </summary>
        public StlBinaryReader(Stream stream)
        {
            if (stream == null) throw new ArgumentNullException("stream");
            this.BaseStream = stream;
        }

        /// <summary>
        /// Read a facet
        /// </summary>
        protected virtual Facet ReadFacet(BinaryReader reader)
        {
            Facet result = new Facet();

            // Read the normal
            result.Normal = ReadVertex(reader);

            // Read the vertices
            for (int i = 0; i < 3; i++)
                result.Vertices.Add(ReadVertex(reader));

            // Read the attribute
            result.AttributeByteCount = reader.ReadUInt16();

            return result;
        }

        /// <summary>
        /// Read a vertex
        /// </summary>
        protected virtual Vertex ReadVertex(BinaryReader reader)
        {
            return new Vertex(
                reader.ReadSingle(), 
                reader.ReadSingle(), 
                reader.ReadSingle()
                );
        }

        /// <summary>
        /// Read a solid
        /// </summary>
        public virtual Solid ReadSolid()
        {
            using(var reader=new BinaryReader(BaseStream))
            {
                Solid result = new Solid();

                // Read the header
                var header = reader.ReadBytes(80);

                // Count the facets
                UInt32 count = reader.ReadUInt32();

                // Read the facets
                for (int i = 0; i < count; i++)
                {
                    result.Facets.Add(ReadFacet(reader));
                }

                return result;
            }
        }

        /// <summary>
        /// Stream
        /// </summary>
        public Stream BaseStream { get; private set; }
    }

}