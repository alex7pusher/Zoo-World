using Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.Movement;
using Modules.ZooWorld._Project.Scripts.Runtime.Infrastructure.Utils;
using UnityEngine;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Infrastructure.Configs
{
    [CreateAssetMenu(fileName = "Jump Movement Config", menuName = ProjectConstants.ProjectName + "/Movement/Jump Movement Config")]
    public sealed class JumpMovementConfig : AnimalMovementConfig
    {
        [SerializeField] private float _jumpInterval = 1f;
        [SerializeField] private float _jumpDistance = 2f;
        [SerializeField] private float _jumpHeight = 1.5f;
        [SerializeField] private float _jumpHeightMultiplier = 5f;
        [SerializeField] private float _jumpForceMultiplier = 3f;
        [SerializeField] private float _groundVelocityThreshold = 0.1f;
        [SerializeField] private float _groundHeightThreshold = 1.5f;

        public float JumpInterval => Mathf.Max(0.05f, _jumpInterval);
        public float JumpDistance => Mathf.Max(0f, _jumpDistance);
        public float JumpHeight => Mathf.Max(0f, _jumpHeight);
        public float JumpHeightMultiplier => Mathf.Max(0f, _jumpHeightMultiplier);
        public float JumpForceMultiplier => Mathf.Max(0f, _jumpForceMultiplier);
        public float GroundVelocityThreshold => Mathf.Max(0f, _groundVelocityThreshold);
        public float GroundHeightThreshold => Mathf.Max(0f, _groundHeightThreshold);

        public override IAnimalMovement CreateMovement()
        {
            return new JumpAnimalMovement(this);
        }
    }
}