using System;
using UnityEngine;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.Spawning
{
    public interface IAnimalSpawnRootProvider : IDisposable
    {
        Transform Root { get; }
    }
}
