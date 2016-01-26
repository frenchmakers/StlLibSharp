using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StlLibrarySharp
{
    /// <summary>
    /// Size of a solid
    /// </summary>
    public class SolidSize
    {
        /// <summary>
        /// Create a new size
        /// </summary>
        public SolidSize()
        {
            this.Min = new Vertex();
            this.Max = new Vertex();
            this.Size = new Vertex();
            this.BoudingDiameter = 0;
        }

        /// <summary>
        /// Minimum values
        /// </summary>
        public Vertex Min { get; set; }

        /// <summary>
        /// Maximum values
        /// </summary>
        public Vertex Max { get; set; }

        /// <summary>
        /// Size values
        /// </summary>
        public Vertex Size { get; set; }

        /// <summary>
        /// Bouding diameter
        /// </summary>
        public double BoudingDiameter { get; set; }
    }
}
