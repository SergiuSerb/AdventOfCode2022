using System;
using System.IO;
using System.Reflection;

namespace Day14.Tools.FileReader
{
    public class FileReader
    {
        public static string[] ReadInputFile(string inputFileName)
        {
            string? executingPath = Path.GetDirectoryName( Assembly.GetEntryAssembly()?.Location );
            string inputPath = $"{executingPath}\\{inputFileName}";

            ReadLines( inputPath, out string[] strings );
    
            return strings;
        }

        private static void ReadLines( string s, out string[] inputLines )
        {
            try
            {
                inputLines = File.ReadAllLines( s );
            }
            catch (Exception)
            {
                throw new ApplicationException( "No lines could be read from input file." );
            }
        }
    }
}