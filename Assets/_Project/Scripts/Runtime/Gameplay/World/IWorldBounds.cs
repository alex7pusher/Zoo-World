using UnityEngine;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.World
{
    public interface IWorldBounds
    {
        bool Contains(Vector3 position);
        Vector3 GetDirectionInside(Vector3 position, Vector3 currentDirection);
    }
}
