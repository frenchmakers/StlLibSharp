namespace StlLibrarySharp
{

    /// <summary>
    /// Represents a STL file reader
    /// </summary>
    public interface IStlReader
    {
        /// <summary>
        /// Read a solid
        /// </summary>
        Solid ReadSolid();
    }

}