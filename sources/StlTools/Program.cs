using STL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace StlTools
{
    class Program
    {
        static string progname;
        static ProgramArgs pArgs = new ProgramArgs();

        /// <summary>
        /// Parse the command line arguments
        /// </summary>
        static void ParseArgs(string[] args)
        {
            if (args == null || args.Length == 0) return;
            for (int i = 0, cargs = args.Length; i < cargs; i++)
            {
                var arg = args[i];
                String opt = null;
                if (arg.StartsWith("--"))
                    opt = arg.Substring(2);
                else if (arg.StartsWith("-"))
                    opt = arg.Substring(1);
                else if (arg.StartsWith("/"))
                    opt = arg.Substring(1);
                if (opt != null)
                {
                    opt = opt.Trim().ToLower();
                    switch (opt)
                    {
                        case "help":
                            pArgs.HelpOption = true;
                            break;
                        case "version":
                            pArgs.VersionOption = true;
                            break;
                        default:
                            throw new InvalidOperationException(String.Format("Unknown option '{0}'", arg));
                    }
                }
                else
                {
                    pArgs.Filename = arg;
                }
            }
        }

        /// <summary>
        /// Print the header
        /// </summary>
        static void PrintHeader()
        {
            var asm = typeof(Program).Assembly;
            var asmName = asm.GetName();
            var cAttr = asm.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false).Cast<AssemblyCopyrightAttribute>().First();
            Console.WriteLine("stl-tools version {0}, {1}", asmName.Version, cAttr.Copyright);
            Console.WriteLine();
        }

        /// <summary>
        /// Print the usage
        /// </summary>
        static void PrintUsage()
        {
            var asm = typeof(Program).Assembly;
            var asmName = asm.GetName();
            var cAttr = asm.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false).Cast<AssemblyCopyrightAttribute>().First();
            Console.WriteLine();
            Console.WriteLine("stl-tools version {0}", asmName.Version);
            Console.WriteLine(cAttr.Copyright);
            Console.WriteLine("Usage: {0} [OPTION, ...] file", progname);
            Console.WriteLine();
            Console.WriteLine("     --help     Display this help and exit");
            Console.WriteLine("     --version  Output version information and exit");
        }

        /// <summary>
        /// Print the try help
        /// </summary>
        static void PrintTryHelp()
        {
            Console.Error.WriteLine();
            Console.Error.WriteLine("Try '{0} --help' for more information.", progname);
        }

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

        static int Main(string[] args)
        {
            progname = System.IO.Path.GetFileName(Environment.GetCommandLineArgs()[0]);
            //progname = Environment.GetCommandLineArgs()[0];

            // Parse and check the arguments
            try
            {
                ParseArgs(args);

                if (pArgs.HelpOption)
                {
                    PrintUsage();
                    return 0;
                }

                if (pArgs.VersionOption)
                {
                    var asm = typeof(Program).Assembly;
                    var asmName = asm.GetName();
                    Console.WriteLine("stl-tools - version {0}", asmName.Version);
                    return 0;
                }

                if (String.IsNullOrWhiteSpace(pArgs.Filename))
                    throw new InvalidOperationException("No input file name given.");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Error.WriteLine("{0}: {1}", progname, ex.GetBaseException().Message);
                Console.ResetColor();
                PrintTryHelp();
                PressKeyToQuit();
                return -1;
            }

            int result = 0;

            // Print header
            PrintHeader();

            try
            {
                // Load solid
                Console.WriteLine("Opening {0}.", pArgs.Filename);
                Solid solid;
                Stopwatch sw = new Stopwatch();
                sw.Start();
                using (var file = File.OpenRead(pArgs.Filename))
                using (var reader = new StlReader(file))
                    solid = reader.ReadSolid();
                sw.Stop();

                // Display file informations
                Console.WriteLine();
                Console.WriteLine("=== File informations");
                Console.WriteLine("File         : {0}", pArgs.Filename);
                Console.WriteLine("Name         : {0}", solid.Name);
                Console.WriteLine("Format       : {0}", solid.Format);
                Console.WriteLine("Time loading : {0} ms", sw.ElapsedMilliseconds);
                Console.WriteLine("Facets       : {0}", solid.Facets.Count);
                Console.WriteLine();

                // Display solid informations
                var solidSize = solid.GetSize();
                var volume = solid.GetSignedVolume();

                Console.WriteLine();
                Console.WriteLine("=== Solid informations");
                Console.WriteLine("Facets : {0,9}", solid.Facets.Count);
                Console.WriteLine("Size X : {0,15:F5} ({1,10:F5} - {2,10:F5})", solidSize.Size.X, solidSize.Min.X, solidSize.Max.X);
                Console.WriteLine("Size Y : {0,15:F5} ({1,10:F5} - {2,10:F5})", solidSize.Size.Y, solidSize.Min.Y, solidSize.Max.Y);
                Console.WriteLine("Size Z : {0,15:F5} ({1,10:F5} - {2,10:F5})", solidSize.Size.Z, solidSize.Min.Z, solidSize.Max.Z);
                Console.WriteLine("Volume : {0,15:F5}", volume);
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Error.WriteLine("{0}: {1}", progname, ex.GetBaseException().Message);
                Console.ResetColor();
                result = -1;
            }
            PressKeyToQuit();
            return result;
        }
    }
}
