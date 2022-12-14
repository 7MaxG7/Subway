using System.Collections.Generic;
using System.Linq;
using Configs.ConfigData;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(menuName = "Configs/" + nameof(SubwayConfig), fileName = nameof(SubwayConfig))]
    public class SubwayConfig : ScriptableObject
    {
        [SerializeField] private List<SubwayLine> _lines;

        [Header("New line parser")]
        [SerializeField] private ParserLine _newLineParserInput;

        public ParserLine NewLineParserInput => _newLineParserInput;

        public List<SubwayLine> Lines => _lines;

        
        public bool CheckColorExists(Color color)
        {
            return _lines.FirstOrDefault(data => data.Color == color) != null;
        }
    }
}