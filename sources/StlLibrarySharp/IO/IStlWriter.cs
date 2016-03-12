namespace STL
{

    /// <summary>
    /// Represents a STL file writer
    /// </summary>
    public interface IStlWriter
    {
        /// <summary>
        /// Write a solid to a file
        /// </summary>
        void WriteSolid(Solid solid);
    }
}