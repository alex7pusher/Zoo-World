using Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.Movement;
using Modules.ZooWorld._Project.Scripts.Runtime.Infrastructure.Utils;
using UnityEngine;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Infrastructure.Configs
{
    [CreateAssetMenu(fileName = "Linear Movement Config", menuName = ProjectConstants.ProjectName + "/Movement/Linear Movement Config")]
    public sealed class LinearMovementConfig : AnimalMovementConfig
    {
        [SerializeField] private float _speed = 2f;

        public float Speed => Mathf.Max(0f, _speed);

        public override IAnimalMovement CreateMovement()
        {
            return new LinearAnimalMovement(this);
        }
    }
}