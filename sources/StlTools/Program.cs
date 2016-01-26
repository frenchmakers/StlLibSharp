using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace StlTools
{
    class Program
    {

        [Conditional("DEBUG")]
        static void PressKeyToQuit()
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine();
                Console.WriteLine("Press a key to quit...");
                Console.ReadKey();
            }
        }

        static void Main(string[] args)
        {

            PressKeyToQuit();
        }
    }
}
