using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StlLibrarySharp.Tests
{
    public static class Utils
    {
        public static Stream OpenDataStream(string filename)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(String.Format("StlLibrarySharp.Tests.data.{0}", filename));
        }
    }
}
