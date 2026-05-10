using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.Movement
{
    public abstract class BaseAnimalMovement : IAnimalMovement
    {
        protected Rigidbody Rigidbody;
        protected Vector3 TargetDirection;
        protected bool IsActive;

        public async UniTask RunAsync(AnimalMovementContext context, CancellationToken cancellationToken)
        {
            Rigidbody = context.Animal.View.Rigidbody;
            IsActive = true;
            OnStarted();

            while (!cancellationToken.IsCancellationRequested && context.Animal.IsAlive)
            {
                FixedExecute(context);
                await UniTask.Yield(PlayerLoopTiming.FixedUpdate, cancellationToken);
            }
        }

        public virtual void Dispose()
        {
            IsActive = false;
        }

        protected virtual void OnStarted()
        {
            TargetDirection = GetRandomDirection();
        }

        protected abstract void FixedExecute(AnimalMovementContext context);

        protected static Vector3 GetRandomDirection()
        {
            float angle = Random.Range(0f, 360f);
            return new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0f, Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
        }

        protected void ApplyRotation(Vector3 direction, float rotationSpeed = 10f)
        {
            if (direction == Vector3.zero || Rigidbody == null)
            {
                return;
            }

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            Quaternion newRotation = Quaternion.Slerp(Rigidbody.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            Rigidbody.MoveRotation(newRotation);
        }

        protected void CheckAndCorrectBounds(AnimalMovementContext context)
        {
            if (Rigidbody == null || context.WorldBounds.Contains(Rigidbody.position))
            {
                return;
            }

            Vector3 directionToCenter = context.WorldBounds.GetDirectionInside(Rigidbody.position, TargetDirection);
            TargetDirection = directionToCenter;

            Vector3 currentVelocity = Rigidbody.linearVelocity;
            Vector3 horizontalVelocity = new(currentVelocity.x, 0f, currentVelocity.z);
            if (horizontalVelocity.sqrMagnitude > 0.001f && Vector3.Dot(horizontalVelocity.normalized, directionToCenter) < 0f)
            {
                Vector3 correctedVelocity = directionToCenter * horizontalVelocity.magnitude;
                Rigidbody.linearVelocity = new Vector3(correctedVelocity.x, currentVelocity.y, correctedVelocity.z);
            }
        }
    }
}