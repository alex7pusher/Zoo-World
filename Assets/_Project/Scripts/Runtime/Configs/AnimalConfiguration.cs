using Modules.Configs;
using Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.Animals;
using Modules.ZooWorld._Project.Scripts.Runtime.Utils;
using UnityEngine;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Configs
{
    [CreateAssetMenu(fileName = "Animal Configuration", menuName = ProjectConstants.ProjectName + "/Animal Configuration")]
    public sealed class AnimalConfiguration : Config
    {
        [SerializeField] private string _id;
        [SerializeField] private AnimalChainType _foodType;
        [SerializeField] private AnimalView _prefab;
        [SerializeField] private AnimalMovementConfig _movement;

        public string Id => _id;
        public AnimalChainType ChainType => _foodType;
        public AnimalView Prefab => _prefab;
        public AnimalMovementConfig Movement => _movement;

        public override void Validate()
        {
            if (string.IsNullOrWhiteSpace(_id))
            {
                Debug.LogError($"{nameof(AnimalConfiguration)} has no id.", this);
            }

            if (_movement == null)
            {
                Debug.LogError($"{nameof(AnimalConfiguration)} for {_id} has no movement definition.", this);
            }

            if (_prefab == null)
            {
                Debug.LogError($"{nameof(AnimalConfiguration)} for {_id} has no prefab.", this);
            }
        }
    }
}