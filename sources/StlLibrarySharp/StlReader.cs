using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace StlLibrarySharp
{
    /// <summary>
    /// Reader for the STL file
    /// </summary>
    public class StlReader : IDisposable    
    {
        private bool ownsStream;

        /// <summary>
        /// Create a new STL reader
        /// </summary>
        /// <param name="stream">Sream to read</param>
        /// <param name="owns">Indicates if the stream is owned by this reader</param>
        public StlReader(Stream stream, bool owns = false)
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
        /// Stream readden
        /// </summary>
        public Stream BaseStream { get; private set; }
    }
}
