using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace STL
{
    /// <summary>
    /// Reader for the STL file
    /// </summary>
    public class StlReader : IStlReader, IDisposable
    {
        /// <summary>
        /// Create a new STL reader
        /// </summary>
        /// <param name="stream">Sream to read</param>
        /// <param name="owns">Indicates if the stream is owned by this reader</param>
        public StlReader(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");
            this.BaseStream = stream;
        }

        /// <summary>
        /// Internal dispose resources
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                this.BaseStream.Dispose();
        }

        /// <summary>
        /// Dispose resources
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Create a reader from the stream
        /// </summary>
        protected virtual IStlReader CreateReader(bool isText)
        {
            return isText ? (IStlReader)new StlTextReader(BaseStream) : (IStlReader)new StlBinaryReader(BaseStream);
        }

        /// <summary>
        /// Check if the file is text format
        /// </summary>
        protected virtual bool CheckIsTextFile()
        {
            // Check if it's a text file
            byte[] buffer = new byte[6];
            if (BaseStream.Read(buffer, 0, buffer.Length) != buffer.Length)
                throw new FormatException("Invalid file format: can't define the type file.");
            BaseStream.Seek(-buffer.Length, SeekOrigin.Current);
            String sTmp = Consts.FileEncoding.GetString(buffer, 0, buffer.Length);
            return String.Equals(sTmp, "solid ", StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Read a solid from the stream
        /// </summary>
        public Solid ReadSolid()
        {
            // Check if it's a text file
            bool isText = CheckIsTextFile();

            // Get the final reader
            var reader = CreateReader(isText);

            // Read a solid
            return reader.ReadSolid();
        }

        /// <summary>
        /// Extract all facets from file
        /// </summary>
        public IEnumerable<Facet> ReadFacets()
        {
            // Check if it's a text file
            bool isText = CheckIsTextFile();

            // Get the final reader
            var reader = CreateReader(isText);

            // Read the facets
            return reader.ReadFacets();
        }

        /// <summary>
        /// Stream readden
        /// </summary>
        public Stream BaseStream { get; private set; }
    }
}
