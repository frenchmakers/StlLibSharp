using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace StlLibrarySharp
{
    /// <summary>
    /// Writer for STL file
    /// </summary>
    public abstract class StlWriter : IDisposable
    {
        private bool ownsStream;

        /// <summary>
        /// Create a new STL writer
        /// </summary>
        /// <param name="stream">Sream to write</param>
        /// <param name="owns">Indicates if the stream is owned by this writer</param>
        public StlWriter(Stream stream, bool owns = false)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");
            this.BaseStream = stream;
            this.ownsStream = owns;
        }

        /// <summary>
        /// Internal dispose resources
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && this.ownsStream)
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
        /// Internal write a solid
        /// </summary>
        protected abstract void InternalWriteSolid(Solid solid);

        /// <summary>
        /// Write a solid
        /// </summary>
        public void WriteSolid(Solid solid)
        {
            if (solid == null) throw new ArgumentNullException("solid");
            InternalWriteSolid(solid);
        }

        /// <summary>
        /// Stream
        /// </summary>
        public Stream BaseStream { get; private set; }
    }

}
