using UnityEngine;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.UI
{
    public interface IScreenPositionService
    {
        Vector3 WorldToScreenPoint(Vector3 worldPosition);
    }
}
