using System.Collections.Generic;
using System.Linq;
using System.Text;
using Configs;
using Configs.ConfigData;
using Utils;

namespace Data
{
    internal class CitySubway
    {
        private readonly Dictionary<Station, HashSet<Station>> _stationNeighbors = new();

        
        public CitySubway(SubwayConfig citySubwayConfig)
        {
            ExtractStations(citySubwayConfig.Lines);
        }

        public Station GetStation(string name)
        {
            return _stationNeighbors.Keys.FirstOrDefault(station => station.Name == name);
        }

        public IEnumerable<Station> GetNeighbors(Station station)
        {
            return _stationNeighbors.TryGetValue(station, out var neighbors) 
                ? neighbors
                : new HashSet<Station>();
        }

        public string GetAllStationsString()
        {
            var stations = new StringBuilder();
            var isFirst = true;
            stations.Append(Constants.CURRENT_STATIONS_PREFIX);
            foreach (var name in _stationNeighbors.Keys.Select(data => data.Name).OrderBy(data => data))
            {
                if (isFirst)
                    isFirst = false;
                else
                    stations.Append(" ");

                stations.Append(name);
            }

            return stations.ToString();
        }

        private void ExtractStations(List<SubwayLine> subwayLines)
        {
            foreach (var line in subwayLines)
            {
                Station previousStation = null;
                foreach (var name in line.StationNames.Select(data => data.ToUpper()))
                {
                    var station = _stationNeighbors.Keys.FirstOrDefault(data => data.Name == name);
                    if (station == null)
                    {
                        station = new Station(line.Color, name);
                        _stationNeighbors.Add(station, new HashSet<Station>());
                    }
                    else
                    {
                        station.RegisterColor(line.Color);
                    }

                    if (previousStation != null)
                    {
                        _stationNeighbors[station].Add(previousStation);
                        _stationNeighbors[previousStation].Add(station);
                    }

                    previousStation = station;
                }
            }
        }
    }
}