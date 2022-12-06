using System;
using System.Collections.Generic;
using System.Linq;

namespace Day6.Models
{
    public class DeviceStream
    {
        private readonly IList<char> stream;
        private Queue<char> startPacketMarker;
        private Queue<char> startMessageMarker;

        public int StartPacketMarkerIndex { get; private set; }
        
        public int StartMessageMarkerIndex { get; private set; }

        public DeviceStream(IList<char> stream)
        {
            this.stream = stream ?? throw new ArgumentNullException(nameof(stream));
            DetermineStartPacketMarker();
            DetermineStartMessageMarker();
        }

        private void DetermineStartPacketMarker()
        {
            startPacketMarker = new Queue<char>();

            int currentIndex = 0;
            foreach (char character in stream)
            {
                if (startPacketMarker.Distinct().Count() == 4)
                {
                    StartPacketMarkerIndex = currentIndex;
                    break;
                }

                if (startPacketMarker.Count == 4)
                {
                    startPacketMarker.Dequeue();
                }

                startPacketMarker.Enqueue(character);
                currentIndex++;
            }
        }
        
        private void DetermineStartMessageMarker()
        {
            startMessageMarker = new Queue<char>();

            int currentIndex = 0;
            foreach (char character in stream)
            {
                if (startMessageMarker.Distinct().Count() == 14)
                {
                    StartMessageMarkerIndex = currentIndex;
                    break;
                }

                if (startMessageMarker.Count == 14)
                {
                    startMessageMarker.Dequeue();
                }

                startMessageMarker.Enqueue(character);
                currentIndex++;
            }
        }
    }
}