using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    internal class Station
    {
        public HashSet<Color> LineColor { get; }
        public string Name { get; }

        
        public Station(Color lineColor, string name)
        {
            LineColor = new HashSet<Color> { lineColor };
            Name = name;
        }

        public void RegisterColor(Color lineColor)
        {
            LineColor.Add(lineColor);
        }
    }
}