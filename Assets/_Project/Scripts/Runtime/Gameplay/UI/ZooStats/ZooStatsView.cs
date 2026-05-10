using System.Collections.Generic;
using Modules.UISystem.Runtime.Scripts.Base;
using Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.Animals;
using UnityEngine;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.UI.ZooStats
{
    public sealed class ZooStatsView : BaseUIView
    {
        [SerializeField] private AnimalCountView _countViewPrefab;
        [SerializeField] private Transform _countViewRoot;

        private readonly Dictionary<AnimalChainType, AnimalCountView> _countViewsByType = new();
        private readonly List<AnimalCountView> _countViews = new();

        public void InitializeCounters(IEnumerable<AnimalChainType> chainTypes)
        {
            ClearCounters();

            foreach (AnimalChainType chainType in chainTypes)
            {
                CreateCountView(chainType);
            }
        }

        public void SetCount(AnimalChainType chainType, int count)
        {
            if (!_countViewsByType.TryGetValue(chainType, out AnimalCountView countView))
            {
                countView = CreateCountView(chainType);
            }

            countView.SetCount(count);
        }

        private AnimalCountView CreateCountView(AnimalChainType chainType)
        {
            if (_countViewPrefab == null)
            {
                return null;
            }
            
            AnimalCountView countView = Instantiate(_countViewPrefab, _countViewRoot);
            countView.Setup(chainType);
            _countViewsByType[chainType] = countView;
            _countViews.Add(countView);

            return countView;
        }

        private void ClearCounters()
        {
            foreach (AnimalCountView countView in _countViews)
            {
                Destroy(countView.gameObject);
            }

            _countViewsByType.Clear();
            _countViews.Clear();
        }
    }
}