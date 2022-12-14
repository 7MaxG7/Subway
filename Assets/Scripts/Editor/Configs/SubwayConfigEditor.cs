using Configs;
using Configs.ConfigData;
using UnityEditor;
using UnityEngine;

namespace Editor.Configs
{
    [CustomEditor(typeof(SubwayConfig))]
    public class SubwayConfigEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var subwayConfig = (SubwayConfig)target;
            if (GUILayout.Button("Parse new line"))
            {
                var stationNames = subwayConfig.NewLineParserInput.StationsLine.Split(' ');
                if (stationNames.Length < 2)
                {
                    Debug.LogError("Subway line must have at least 2 stations");
                    return;
                }

                if (subwayConfig.CheckColorExists(subwayConfig.NewLineParserInput.Color))
                {
                    Debug.LogError("Line with this color is exists");
                    return;
                }
                
                subwayConfig.Lines.Add(new SubwayLine(subwayConfig.NewLineParserInput.Color, stationNames));
            }
            
            EditorUtility.SetDirty(target);
        }
    }
}