using System;
using System.Collections.Generic;
using System.IO;
using Mono.Cecil;

namespace Unity.UNetWeaver
{
    public static class Log
    {
        public static Action<string> WarningMethod;
        public static Action<string> ErrorMethod;

        public static void Warning(string msg)
        {
            WarningMethod("UNetWeaver warning: " + msg);
        }

        public static void Error(string msg)
        {
            ErrorMethod("UNetWeaver error: " + msg);
        }
    }

    public class Program
    {
        public static bool Process(string unityEngine, string unetDLL, string outputDirectory, string[] assemblies, string[] extraAssemblyPaths, IAssemblyResolver assemblyResolver, Action<string> printWarning, Action<string> printError)
        {
            Log.WarningMethod = printWarning;
            Log.ErrorMethod = printError;
            string message = "";
            message += "unityEngine="+unityEngine + "\n";
            message += "unetDLL="+unetDLL + "\n";
            message += "outputDirectory="+outputDirectory + "\n";
            message += "assemblies="+assemblies.Length + "\n";
            foreach (string assembly in assemblies) message += "  " + assembly + "\n";
            message += "extraAssemblyPaths="+extraAssemblyPaths.Length + "\n";
            foreach (string assembly in extraAssemblyPaths) message += "  " + assembly + "\n";
            Log.Warning(message);

            /*CheckDLLPath(unityEngine);
            CheckDLLPath(unetDLL);
            CheckOutputDirectory(outputDirectory);
            CheckAssemblies(assemblies);
            return Weaver.WeaveAssemblies(assemblies, extraAssemblyPaths, assemblyResolver, outputDirectory, unityEngine, unetDLL);*/
            return true;
        }

        private static void CheckDLLPath(string path)
        {
            if (!File.Exists(path))
                throw new Exception("dll could not be located at " + path + "!");
        }

        private static void CheckAssemblies(IEnumerable<string> assemblyPaths)
        {
            foreach (var assemblyPath in assemblyPaths)
                CheckAssemblyPath(assemblyPath);
        }

        private static void CheckAssemblyPath(string assemblyPath)
        {
            if (!File.Exists(assemblyPath))
                throw new Exception("Assembly " + assemblyPath + " does not exist!");
        }

        private static void CheckOutputDirectory(string outputDir)
        {
            if (!Directory.Exists(outputDir))
                Directory.CreateDirectory(outputDir);
        }
    }
}
