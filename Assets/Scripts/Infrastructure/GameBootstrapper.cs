using Configs;
using Data;
using UnityEngine;

namespace Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] private SubwayConfig _citySubwayConfig;
        [SerializeField] private PathFinderUi _pathFinderUi;

        private PathFinder _pathFinder;


        public void Awake()
        {
            _pathFinder = new PathFinder(_pathFinderUi);
        }

        public void Start()
        {
            InitializeSubway();
        }

        private void InitializeSubway()
        {
            var citySubway = new CitySubway(_citySubwayConfig);
            _pathFinder.Init(citySubway);
        }
    }
}