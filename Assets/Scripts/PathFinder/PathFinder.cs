using System.Collections.Generic;
using System.Linq;
using Data;
using Utils;

namespace Infrastructure
{
    internal class PathFinder
    {

        private readonly PathFinderUi _pathFinderUi;
        private CitySubway _citySubway;

        
        public PathFinder(PathFinderUi pathFinderUi)
        {
            _pathFinderUi = pathFinderUi;
            _pathFinderUi.OnFindPathRequest += ShowPath;
        }

        public void Init(CitySubway citySubway)
        {
            _citySubway = citySubway;
            _pathFinderUi.ShowCurrentStations(_citySubway.GetAllStationsString());
        }

        private void ShowPath(string start, string end)
        {
            var pathInfo = FindPath(start, end);
            _pathFinderUi.ShowPath(pathInfo);
        }

        private PathInfo FindPath(string start, string end)
        {
            var startStation = _citySubway.GetStation(start);
            if (startStation == null)
            {
                return PathInfo.GeneratePathError(Constants.WRONG_START_STATION_NAME_ERROR);
            }
            
            var endStation = _citySubway.GetStation(end);
            if (endStation == null)
            {
                return PathInfo.GeneratePathError(Constants.WRONG_END_STATION_NAME_ERROR);
            }

            var previousStations = new Dictionary<Station, Station> { [startStation] = null };
            var stationsInProgress = new Queue<Station>();
            stationsInProgress.Enqueue(startStation);
            while (stationsInProgress.TryDequeue(out var station))
            {
                if (station == endStation)
                    return new PathInfo(endStation, previousStations);

                foreach (var neighbor in _citySubway.GetNeighbors(station).Where(data => !previousStations.ContainsKey(data)))
                {
                    stationsInProgress.Enqueue(neighbor);
                    previousStations.Add(neighbor, station);
                }
            }
            
            return PathInfo.GeneratePathError(Constants.PATH_NOT_FOUND_ERROR);
        }
    }
}