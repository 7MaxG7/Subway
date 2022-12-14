using System;
using UnityEngine;

namespace Configs.ConfigData
{
    [Serializable]
    public class SubwayLine
    {
        [SerializeField] private Color _lineColor;
        [SerializeField] private string[] _stationNames;

        public Color Color => _lineColor;
        public string[] StationNames => _stationNames;


        public SubwayLine(Color lineColor, string[] stationNames)
        {
            _lineColor = lineColor;
            _stationNames = stationNames;
        }
    }
}