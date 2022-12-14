using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utils;

namespace Data
{
    internal class PathInfo
    {
        public readonly Stack<Station> StationsPath = new();
        public int ColorChanges { get; }

        public string ErrorMessage { get; set; }
        public bool IsError => !string.IsNullOrEmpty(ErrorMessage);


        private PathInfo()
        {
        }
        
        public PathInfo(Station target, IReadOnlyDictionary<Station, Station> checkedStations)
        {
            var currentLineColor = target.LineColor;
            var currentStation = target;
            while (currentStation != null)
            {
                StationsPath.Push(currentStation);
                if (checkedStations[currentStation] != null)
                {
                    currentLineColor = currentLineColor
                        .Intersect(checkedStations[currentStation].LineColor)
                        .ToHashSet();
                    if (currentLineColor.Count == 0)
                    {
                        currentLineColor = checkedStations[currentStation].LineColor;
                        ColorChanges++;
                    }
                }

                currentStation = checkedStations[currentStation];
            }
        }

        public static PathInfo GeneratePathError(string errorMessage)
        {
            return new PathInfo
            {
                ErrorMessage = errorMessage
            };
        }

        public string GetPathString()
        {
            var isFirst = true;
            var result = new StringBuilder();
            result.Append(Constants.STATIONS_PATH_HEADER);
            while (StationsPath.TryPop(out var station))
            {
                if (isFirst)
                    isFirst = false;
                else
                    result.Append(Constants.STATIONS_PATH_SEPARATOR);

                result.Append(station.Name);
            }

            result.Append(string.Format(Constants.LINES_CHANGES_TEMPLATE, ColorChanges));
            return result.ToString();
        }
    }
}