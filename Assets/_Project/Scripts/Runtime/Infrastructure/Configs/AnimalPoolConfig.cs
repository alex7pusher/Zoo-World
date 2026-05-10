using Modules.Configs;
using Modules.ZooWorld._Project.Scripts.Runtime.Infrastructure.Utils;
using UnityEngine;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Infrastructure.Configs
{
    [CreateAssetMenu(fileName = "Animal Pool Config", menuName = ProjectConstants.ProjectName + "/Animal Pool Config")]
    public sealed class AnimalPoolConfig : Config
    {
        [SerializeField] private int _maxAliveAnimals = 30;
        [SerializeField] private float _deadAnimalDespawnDelay = 0.25f;
        [SerializeField] private int _animalPoolDefaultCapacity = 5;
        [SerializeField] private int _animalPoolMaxSize = 30;

        public int MaxAliveAnimals => Mathf.Max(1, _maxAliveAnimals);
        public float DeadAnimalDespawnDelay => Mathf.Max(0f, _deadAnimalDespawnDelay);
        public int AnimalPoolDefaultCapacity => Mathf.Max(1, _animalPoolDefaultCapacity);
        public int AnimalPoolMaxSize => Mathf.Max(AnimalPoolDefaultCapacity, _animalPoolMaxSize);
    }
}
