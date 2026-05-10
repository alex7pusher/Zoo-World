using Modules.ZooWorld._Project.Scripts.Runtime.Configs;
using UnityEngine;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.Movement
{
    public sealed class LinearAnimalMovement : BaseAnimalMovement
    {
        private readonly LinearMovementConfig _config;

        public LinearAnimalMovement(LinearMovementConfig config)
        {
            _config = config;
        }

        protected override void FixedExecute(AnimalMovementContext context)
        {
            if (!IsActive || Rigidbody == null)
            {
                return;
            }

            CheckAndCorrectBounds(context);

            Vector3 targetVelocity = TargetDirection * _config.Speed;
            Rigidbody.linearVelocity = new Vector3(targetVelocity.x, Rigidbody.linearVelocity.y, targetVelocity.z);

            if (TargetDirection != Vector3.zero)
            {
                ApplyRotation(TargetDirection);
            }
        }
    }
}