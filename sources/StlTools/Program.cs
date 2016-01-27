using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                Console.Error.WriteLine("{0}: {1}", progname, ex.GetBaseException().Message);
                PrintTryHelp();
                PressKeyToQuit();
                return -1;
            }

            // Print
            PrintHeader();
            

            PressKeyToQuit();
            return 0;
        }
    }
}
