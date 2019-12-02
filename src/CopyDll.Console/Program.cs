using System;
using System.Diagnostics;
using System.IO;

namespace CopyDll.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var file = "CobreBemX.dll";
            var sourcePath = GetSourcePath(file);
            var path32 = Environment.SystemDirectory;
            var path64 = @"C:\Windows\SysWOW64";
            var fullPath32 = Path.Combine(path32, file);
            var fullPath64 = Path.Combine(path64, file);

            File.Copy(sourcePath, fullPath64, true);
            File.Copy(sourcePath, fullPath32, true);
            RegisterDll(path64, file);
            RegisterDll(path32, file);
        }

        private static void RegisterDll(string path, string file)
        {
            var command = $@"{path} {file}";
            var procStartInfo = new ProcessStartInfo("cmd", "/c " + command);
            var proc = new Process();
            procStartInfo.RedirectStandardOutput = true;
            procStartInfo.UseShellExecute = false;
            procStartInfo.CreateNoWindow = true;
            proc.StartInfo = procStartInfo;
            proc.Start();
        }

        private static string GetSourcePath(string file)
        {
            var curPath = Directory.GetCurrentDirectory();
            var fullFileName = curPath.Replace("CopyDll.Console\\bin\\Debug", "lib");
            var sourcePath = Path.Combine(fullFileName, file);
            return sourcePath;
        }
    }
}
