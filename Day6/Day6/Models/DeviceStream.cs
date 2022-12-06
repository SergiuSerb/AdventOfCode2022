using System;
using System.Collections.Generic;
using System.Linq;

namespace Day6.Models
{
    public class DeviceStream
    {
        public IList<char> Stream { get; }

        public Queue<char> StartPacketMarker { get; set; }
        
        public Queue<char> StartMessageMarker { get; set; }

        public int StartPacketMarkerIndex { get; set; }
        
        public int StartMessageMarkerIndex { get; set; }

        public DeviceStream(IList<char> stream)
        {
            Stream = stream ?? throw new ArgumentNullException(nameof(stream));
            DetermineStartPacketMarker();
            DetermineStartMessageMarker();
        }

        private void DetermineStartPacketMarker()
        {
            StartPacketMarker = new Queue<char>();

            int currentIndex = 0;
            foreach (char character in Stream)
            {
                if (StartPacketMarker.Distinct().Count() == 4)
                {
                    StartPacketMarkerIndex = currentIndex;
                    break;
                }

                if (StartPacketMarker.Count == 4)
                {
                    StartPacketMarker.Dequeue();
                }

                StartPacketMarker.Enqueue(character);
                currentIndex++;
            }
        }
        
        private void DetermineStartMessageMarker()
        {
            StartMessageMarker = new Queue<char>();

            int currentIndex = 0;
            foreach (char character in Stream)
            {
                if (StartMessageMarker.Distinct().Count() == 14)
                {
                    StartMessageMarkerIndex = currentIndex;
                    break;
                }

                if (StartMessageMarker.Count == 14)
                {
                    StartMessageMarker.Dequeue();
                }

                StartMessageMarker.Enqueue(character);
                currentIndex++;
            }
        }
    }
}