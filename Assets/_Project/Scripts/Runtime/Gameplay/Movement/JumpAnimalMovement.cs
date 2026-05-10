using Modules.ZooWorld._Project.Scripts.Runtime.Infrastructure.Configs;
using UnityEngine;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.Movement
{
    public sealed class JumpAnimalMovement : BaseAnimalMovement
    {
        private readonly JumpMovementConfig _config;
        private float _jumpTimer;

        public JumpAnimalMovement(JumpMovementConfig config)
        {
            _config = config;
        }

        protected override void OnStarted()
        {
            base.OnStarted();
            _jumpTimer = _config.JumpInterval;
        }

        protected override void FixedExecute(AnimalMovementContext context)
        {
            if (!IsActive || Rigidbody == null)
            {
                return;
            }

            CheckAndCorrectBounds(context);

            if (IsGrounded())
            {
                _jumpTimer -= Time.fixedDeltaTime;

                if (_jumpTimer <= 0f)
                {
                    PerformJump();
                    _jumpTimer = _config.JumpInterval;
                }
            }

            if (TargetDirection != Vector3.zero)
            {
                ApplyRotation(TargetDirection);
            }
        }

        private bool IsGrounded()
        {
            return Mathf.Abs(Rigidbody.linearVelocity.y) < _config.GroundVelocityThreshold
                && Rigidbody.position.y < _config.GroundHeightThreshold;
        }

        private void PerformJump()
        {
            TargetDirection = GetRandomDirection();
            Vector3 jumpVector = TargetDirection * (_config.JumpDistance * _config.JumpForceMultiplier)
                + Vector3.up * (_config.JumpHeight * _config.JumpHeightMultiplier);
            Rigidbody.AddForce(jumpVector, ForceMode.Impulse);
        }
    }
}