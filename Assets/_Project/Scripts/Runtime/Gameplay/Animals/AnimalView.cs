using System;
using UniRx;
using UnityEngine;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.Animals
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public sealed class AnimalView : MonoBehaviour
    {
        [SerializeField] private Transform _visualRoot;
        [SerializeField] private Transform _labelAnchor;

        private readonly Subject<AnimalCollision> _collisions = new Subject<AnimalCollision>();

        public Rigidbody Rigidbody { get; private set; }
        public Collider Collider { get; private set; }
        public Transform LabelAnchor => _labelAnchor != null ? _labelAnchor : transform;
        public IObservable<AnimalCollision> Collisions => _collisions;
        public IAnimal Controller { get; private set; }

        public void Initialize(IAnimal controller)
        {
            Controller = controller;
            Rigidbody ??= GetComponent<Rigidbody>();
            Collider ??= GetComponent<Collider>();

            if (_visualRoot == null)
            {
                _visualRoot = transform;
            }
        }

        public void ResetForPool()
        {
            Controller = null;

            if (Rigidbody != null)
            {
                Rigidbody.linearVelocity = Vector3.zero;
                Rigidbody.angularVelocity = Vector3.zero;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            AnimalView other = collision.collider.GetComponentInParent<AnimalView>();
            if (other == null || other == this)
            {
                return;
            }

            _collisions.OnNext(new AnimalCollision(this, other));
        }

        private void OnDestroy()
        {
            _collisions.Dispose();
        }
    }
}