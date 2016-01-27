using System.Collections.Generic;

namespace STL
{

    /// <summary>
    /// Represents a STL file reader
    /// </summary>
    public interface IStlReader
    {

        /// <summary>
        /// Read a solid from file
        /// </summary>
        Solid ReadSolid();

        /// <summary>
        /// Extract all facets from file
        /// </summary>
        IEnumerable<Facet> ReadFacets();

    }

}