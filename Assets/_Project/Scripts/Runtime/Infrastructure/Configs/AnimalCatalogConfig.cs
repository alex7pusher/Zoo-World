using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Modules.Configs;
using Modules.ZooWorld._Project.Scripts.Runtime.Infrastructure.Utils;
using UnityEngine;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Infrastructure.Configs
{
    [CreateAssetMenu(fileName = "Animal Catalog Config", menuName = ProjectConstants.ProjectName + "/Animal Catalog Config")]
    public sealed class AnimalCatalogConfig : Config
    {
        [SerializeField] private List<AnimalConfiguration> _animals = new();

        private Dictionary<string, AnimalConfiguration> _configsById;

        public override void Validate()
        {
            if (_animals.Count == 0)
            {
                Debug.LogWarning($"{nameof(AnimalCatalogConfig)} has no animals.", this);
            }
        }

        [PublicAPI]
        public bool TryGetConfig(string id, out AnimalConfiguration configuration)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                configuration = null;
                return false;
            }

            EnsureIndex();
            return _configsById.TryGetValue(id, out configuration);
        }

        public bool TryGetRandomConfig(out AnimalConfiguration configuration)
        {
            configuration = null;

            if (_animals.Count == 0)
                return false;

            int startIndex = UnityEngine.Random.Range(0, _animals.Count);
            for (int offset = 0; offset < _animals.Count; offset++)
            {
                int index = (startIndex + offset) % _animals.Count;
                AnimalConfiguration animal = _animals[index];

                if (animal == null || string.IsNullOrWhiteSpace(animal.Id))
                    continue;

                configuration = animal;
                return true;
            }

            return false;
        }

        private void EnsureIndex()
        {
            if (_configsById != null)
            {
                return;
            }

            _configsById = new Dictionary<string, AnimalConfiguration>(StringComparer.Ordinal);
            foreach (AnimalConfiguration animal in _animals)
            {
                if (animal == null || string.IsNullOrWhiteSpace(animal.Id))
                {
                    continue;
                }

                _configsById.TryAdd(animal.Id, animal);
            }
        }
    }
}