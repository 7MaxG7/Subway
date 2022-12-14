using System;
using UnityEngine;

namespace Configs.ConfigData
{
    [Serializable]
    public class ParserLine
    {
        [SerializeField] private Color _lineColor;
        [Tooltip("Enter station names separated by spaces")]
        [SerializeField] private string _stationsLine;
        
        public Color Color => _lineColor;
        public string StationsLine => _stationsLine;
    }
}