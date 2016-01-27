using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StlTools
{
    /// <summary>
    /// Arguments
    /// </summary>
    public class ProgramArgs
    {
        /// <summary>
        /// Create args
        /// </summary>
        public ProgramArgs()
        {
            HelpOption = false;
            VersionOption = false;
            Filename = null;
        }
        /// <summary>
        /// Help option
        /// </summary>
        public bool HelpOption { get; set; }
        /// <summary>
        /// Version option
        /// </summary>
        public bool VersionOption { get; set; }
        /// <summary>
        /// Name of the file to process
        /// </summary>
        public String Filename { get; set; }
    }
}
