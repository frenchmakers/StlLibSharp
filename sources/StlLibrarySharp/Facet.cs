using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StlLibrarySharp
{

    /// <summary>
    /// Represents a facet in a solid object.
    /// </summary>
    public class Facet : IEquatable<Facet>
    {

        /// <summary>
        /// Create a new empty <see cref="facet"/>.
        /// </summary>
        public Facet()
        {
            this.Normal = new Vertex();
            this.Vertices = new List<Vertex>();
        }

        /// <summary>
        /// Create a new facet.
        /// </summary>
        /// <param name="normal">The facet orientation</param>
        /// <param name="vertices">The location</param>
        /// <param name="attributeByteCount">Additional data</param>
        public Facet(Vertex normal, IEnumerable<Vertex> vertices, UInt16 attributeByteCount = 0)
            : this()
        {
            if (normal != null) this.Normal = normal;
            if (vertices != null) this.Vertices = vertices.ToList();
            this.AttributeByteCount = attributeByteCount;
        }

        /// <summary>
        /// Determines if the <paramref name="obj"/> is equals to this <see cref="Facet"/>.
        /// </summary>
        /// <param name="obj">Object to compare</param>
        public override bool Equals(object obj)
        {
            if (obj is Facet)
                return Equals((Facet)obj);
            return base.Equals(obj);
        }

        /// <summary>
        /// Determines if the <paramref name="other"/> facet is equals to this <see cref="Facet"/>.
        /// </summary>
        /// <param name="other">Facet to compare</param>
        public bool Equals(Facet other)
        {
            if (other == null) return false;
            if (other == this) return true;
            if (this.Normal == null || other.Normal == null) return false;
            if (this.Vertices == null || other.Vertices == null) return false;
            return
                this.Normal.Equals(other.Normal)
                && this.AttributeByteCount == other.AttributeByteCount
                && this.Vertices.Count == other.Vertices.Count
                && Enumerable.Range(0, this.Vertices.Count).All(i => this.Vertices[i].Equals(other.Vertices[i]))
                ;
        }

        /// <summary>
        /// Returns the <see cref="Facet"/> hashcode.
        /// </summary>
        public override int GetHashCode()
        {
            return
                (this.Normal != null ? this.Normal.GetHashCode() : 0)
                ^ (this.Vertices != null ? this.Vertices.Count.GetHashCode() : 0)
                ^ this.AttributeByteCount.GetHashCode()
                ;
        }

        /// <summary>
        /// Returns the string representation of this <see cref="Facet"/>
        /// </summary>
        public override string ToString()
        {
            return String.Format("facet({0})", this.Normal);
        }

        /// <summary>
        /// Vertex for the facet orientation
        /// </summary>
        public Vertex Normal { get; set; }

        /// <summary>
        /// Location of the facet
        /// </summary>
        public IList<Vertex> Vertices { get; set; }

        /// <summary>
        /// Additional data
        /// </summary>
        /// <remarks>Defined only in the binary format.</remarks>
        public UInt16 AttributeByteCount { get; set; }
    }

}
