using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.Spawning
{
    [UsedImplicitly]
    public sealed class AnimalSpawnRootProvider : IAnimalSpawnRootProvider
    {
        private readonly GameObject _rootObject = new("Animals");

        public Transform Root => _rootObject.transform;

        public void Dispose()
        {
            if (_rootObject != null)
            {
                Object.Destroy(_rootObject);
            }
        }
    }
}
