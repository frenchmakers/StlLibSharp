﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace StlLibrarySharp
{
    /// <summary>
    /// A binary file format writer
    /// </summary>
    public class StlBinaryWriter : StlWriter
    {
        /// <summary>
        /// Create a new binary writer
        /// </summary>
        public StlBinaryWriter(Stream stream, bool owns = false) 
            : base(stream, owns)
        {
            this.Header = "Binary STL generated by the STL Library Sharp.";
        }

        /// <summary>
        /// Build the header
        /// </summary>
        protected virtual byte[] BuildHeader()
        {
            var enc = Encoding.GetEncoding("ASCII");
            return enc.GetBytes(Header ?? String.Empty);
        }

        /// <summary>
        /// Write a facet
        /// </summary>
        protected virtual void WriteFacet(Facet facet, BinaryWriter writer)
        {
            // Write the normal
            WriteVertex(facet.Normal, writer);

            // Write each vertice
            foreach (var vertice in facet.Vertices)
                WriteVertex(vertice, writer);

            // Write the attribute byte count.
            writer.Write(facet.AttributeByteCount);
        }

        /// <summary>
        /// Write a vertex
        /// </summary>
        protected virtual void WriteVertex(Vertex vertex, BinaryWriter writer)
        {
            writer.Write(vertex.X);
            writer.Write(vertex.Y);
            writer.Write(vertex.Z);
        }

        /// <summary>
        /// Internal write a solid
        /// </summary>
        protected override void InternalWriteSolid(Solid solid)
        {
            using(var writer=new BinaryWriter(BaseStream))
            {
                // Writer header
                var headerBytes = new byte[80];
                var hd = BuildHeader();
                Buffer.BlockCopy(hd, 0, headerBytes, 0, Math.Min(80, hd.Length));
                writer.Write(headerBytes);

                // Facets count
                writer.Write((UInt32)solid.Facets.Count);

                // Foreach facet
                foreach (var facet in solid.Facets)
                    WriteFacet(facet, writer);
            }
        }

        /// <summary>
        /// Header to write
        /// </summary>
        public String Header { get; set; }

    }
}
