using System.Collections.Generic;
using JetBrains.Annotations;
using Modules.ZooWorld._Project.Scripts.Runtime.Infrastructure.Configs;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.Animals
{
    [UsedImplicitly]
    public sealed class AnimalPool : IAnimalPool
    {
        private readonly AnimalPoolConfig _config;
        private readonly Dictionary<string, ObjectPool<AnimalView>> _poolsBySpeciesId = new();
        private readonly Dictionary<AnimalView, ObjectPool<AnimalView>> _poolsByView = new();

        public AnimalPool(AnimalPoolConfig config)
        {
            _config = config;
        }

        public AnimalView Get(AnimalSpawnData spawnData)
        {
            ObjectPool<AnimalView> pool = GetPool(spawnData);
            AnimalView view = pool.Get();
            _poolsByView[view] = pool;
            Transform viewTransform = view.transform;
            viewTransform.SetParent(spawnData.Parent, false);
            viewTransform.SetPositionAndRotation(spawnData.Position, spawnData.Rotation);
            return view;
        }

        public void Release(IAnimal animal)
        {
            if (animal == null || animal.View == null)
            {
                return;
            }

            if (!_poolsByView.TryGetValue(animal.View, out ObjectPool<AnimalView> pool))
            {
                return;
            }

            pool.Release(animal.View);
        }

        private ObjectPool<AnimalView> GetPool(AnimalSpawnData spawnData)
        {
            if (spawnData.Configuration.Prefab == null)
            {
                throw new System.InvalidOperationException($"{spawnData.Configuration.Id} has no animal prefab assigned.");
            }

            if (_poolsBySpeciesId.TryGetValue(spawnData.Configuration.Id, out ObjectPool<AnimalView> pool))
            {
                return pool;
            }

            pool = CreatePool(spawnData);
            _poolsBySpeciesId.Add(spawnData.Configuration.Id, pool);
            
            return pool;
        }

        private ObjectPool<AnimalView> CreatePool(AnimalSpawnData spawnData)
        {
            AnimalView prefab = spawnData.Configuration.Prefab;
            return new ObjectPool<AnimalView>(
                createFunc: () => CreateView(prefab, spawnData.Parent),
                actionOnGet: OnGet,
                actionOnRelease: OnRelease,
                actionOnDestroy: OnDestroy,
                collectionCheck: true,
                defaultCapacity: _config.AnimalPoolDefaultCapacity,
                maxSize: _config.AnimalPoolMaxSize);
        }

        private AnimalView CreateView(AnimalView prefab, Transform parent)
        {
            return Object.Instantiate(prefab, parent);
        }

        private static void OnGet(AnimalView view)
        {
            view.gameObject.SetActive(true);
        }

        private static void OnRelease(AnimalView view)
        {
            view.ResetForPool();
            view.gameObject.SetActive(false);
        }

        private void OnDestroy(AnimalView view)
        {
            _poolsByView.Remove(view);
            Object.Destroy(view.gameObject);
        }
    }
}
