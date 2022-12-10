using System;

namespace Day10.Models
{
    internal class GraphicalProcessingUnit
    {
        private const int _canvasWidth = 40;
        private const int _canvasHeight = 6;

        private char[] _canvas;

        public GraphicalProcessingUnit()
        {
            InitializeCanvas();
        }

        private void InitializeCanvas()
        {
            _canvas = new char[_canvasHeight * _canvasWidth];

            for ( int pixelIndex = 0; pixelIndex < _canvas.Length; pixelIndex++ )
            {
                _canvas[pixelIndex] = '.';
            }
        }

        public void PrintCanvas()
        {
            for ( int rowIndex = 0; rowIndex < _canvasHeight; rowIndex++ )
            {
                for ( int columnIndex = 0; columnIndex < _canvasWidth; columnIndex++ )
                {
                    Console.Write(_canvas[_canvasWidth * rowIndex + columnIndex]);
                }
                Console.WriteLine();
            }
        }

        public void Cycle( int currentCycle, Registry registry )
        {
            if ( Math.Abs(registry.X - (currentCycle%_canvasWidth - 1)) <= 1 )
            {
                _canvas[currentCycle - 1] = '#';
            }
        }
    }
}