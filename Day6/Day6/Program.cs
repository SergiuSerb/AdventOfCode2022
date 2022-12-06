using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Day6.Models;

namespace Day6
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] inputLines = ReadInputFile();
            DeviceStream stream = new DeviceStream(inputLines[0].ToList());
            DetermineStartOfPackets(stream);
            DetermineStartOfMessage(stream);
        }

        private static void DetermineStartOfMessage(DeviceStream stream)
        {
            Console.WriteLine($"The start of message is at {stream.StartMessageMarkerIndex}.");
        }

        private static void DetermineStartOfPackets(DeviceStream stream)
        {
            Console.WriteLine($"The start of packets is at {stream.StartPacketMarkerIndex}.");
        }

        private static string[] ReadInputFile()
        {
            string? executingPath = Path.GetDirectoryName( Assembly.GetEntryAssembly()?.Location );
            const string inputFileName = "input.txt";
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
                throw new ArgumentNullException( "No lines could be read from input file." );
            }
        }
    }
}