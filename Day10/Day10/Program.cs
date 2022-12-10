using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Day10.Models;
using Day10.Models.Commands;

namespace Day10
{
    internal static class Program
    {
        private static void Main( string[] args )
        {
            string[] inputLines = ReadInputFile();
            CommandBatch batch = CreateBatch( inputLines );
            CentralProcessingUnit cpu = RunBatch( batch );
            DetermineRegistrySum( cpu );
            PrintCanvasToConsole( cpu );
        }

        private static void PrintCanvasToConsole( CentralProcessingUnit cpu )
        {
            cpu.PrintGpuCanvas();
        }

        private static void DetermineRegistrySum( CentralProcessingUnit cpu )
        {
            IList<int> indices = new List<int>() { 20, 60, 100, 140, 180, 220 };

            int sum = cpu.GetRegistrySignalStrengthSumAtIndices(indices);
            Console.WriteLine($"The signal strength at the specified indices is {sum}.");
        }

        private static CentralProcessingUnit RunBatch( CommandBatch batch )
        {
            GraphicalProcessingUnit gpu = new GraphicalProcessingUnit();
            CentralProcessingUnit cpu = new CentralProcessingUnit(gpu);
            cpu.RunBatch(batch);

            return cpu;
        }

        private static CommandBatch CreateBatch( string[] inputLines )
        {
            CommandBatch batch = new CommandBatch();
            const char separator = ' ';
            foreach ( string inputLine in inputLines )
            {
                string[] splitLine = inputLine.Split( separator );

                int argument = 0;
                if ( splitLine.GetLength(0) > 1 )
                {
                    argument = int.Parse( splitLine[1] );
                }

                CommandBase command =
                    string.Equals( splitLine[0], AddToRegistryCommand.Keyword, StringComparison.Ordinal )
                        ? new AddToRegistryCommand( argument )
                        : new NoOperationCommandBase();

                batch.AddCommand(command);
            }

            return batch;
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