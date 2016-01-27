using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace STL
{
    /// <summary>
    /// A vertex object
    /// </summary>
    public class Vertex : IEquatable<Vertex>
    {

        /// <summary>
        /// Creates a new empty <see cref="Vertex"/>.
        /// </summary>
        public Vertex() { }

        /// <summary>
        /// Creates a new <see cref="Vertex"/> with his coordinates.
        /// </summary>
        /// <param name="x">The <see cref="X"/> coordinate</param>
        /// <param name="y">The <see cref="Y"/> coordinate</param>
        /// <param name="z">The <see cref="Z"/> coordinate</param>
        public Vertex(float x, float y, float z)
            : this()
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        /// <summary>
        /// Create a <see cref="Vertex"/> from an another <see cref="Vertex"/>.
        /// </summary>
        /// <param name="source">The <see cref="Vertex"/> to copy</param>
        public Vertex(Vertex source)
        {
            if (source != null)
            {
                this.X = source.X;
                this.Y = source.Y;
                this.Z = source.Z;
            }
        }

        /// <summary>
        /// Determines if the <paramref name="obj"/> is equals to this <see cref="Vertex"/>.
        /// </summary>
        /// <param name="obj">Object to compare</param>
        public override bool Equals(object obj)
        {
            if (obj is Vertex)
                return Equals((Vertex)obj);
            return base.Equals(obj);
        }

        /// <summary>
        /// Determines if the <paramref name="other"/> vertex is equals to this <see cref="Vertex"/>.
        /// </summary>
        /// <param name="other">Vertex to compare</param>
        public bool Equals(Vertex other)
        {
            if (other == null) return false;
            return this.X.Equals(other.X)
                && this.Y.Equals(other.Y)
                && this.Z.Equals(other.Z);
        }

        /// <summary>
        /// Returns the <see cref="Vertex"/> hashcode.
        /// </summary>
        public override int GetHashCode()
        {
            return 
                this.X.GetHashCode()
                ^ this.Y.GetHashCode()
                ^ this.Z.GetHashCode()
                ;
        }

        /// <summary>
        /// Returns the string representation of this <see cref="Vertex"/>
        /// </summary>
        public override string ToString()
        {
            return String.Format(CultureInfo.InvariantCulture, "[{0}, {1}, {2}]", this.X, this.Y, this.Z);
        }

        /// <summary>
        /// The X coordinate of this <see cref="Vertex"/>.
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// The Y coordinate of this <see cref="Vertex"/>.
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        /// The Z coordinate of this <see cref="Vertex"/>.
        /// </summary>
        public float Z { get; set; }

    }
}
