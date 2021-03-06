﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace STL
{

    /// <summary>
    /// Represents a solid object defined by facets.
    /// </summary>
    public class Solid
    {
        /// <summary>
        /// Create a new solid
        /// </summary>
        public Solid(String name = null, IEnumerable<Facet> source = null)
        {
            this.Name = name;
            this.Facets = source != null ? source.ToList() : new List<Facet>();
            this.Format = SolidFormat.Memory;
        }

        /// <summary>
        /// Name of the solid
        /// </summary>
        /// <remarks>Not used in the binary files.</remarks>
        public String Name { get; set; }

        /// <summary>
        /// Loaded/Saved format
        /// </summary>
        public SolidFormat Format { get; set; }

        /// <summary>
        /// List of the facets
        /// </summary>
        public IList<Facet> Facets { get; private set; }
    }

}
