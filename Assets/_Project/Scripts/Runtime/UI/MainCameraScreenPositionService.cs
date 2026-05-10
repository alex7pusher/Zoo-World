using JetBrains.Annotations;
using UnityEngine;

namespace Modules.ZooWorld._Project.Scripts.Runtime.UI
{
    [UsedImplicitly]
    public sealed class MainCameraScreenPositionService : IScreenPositionService
    {
        private readonly Camera _camera = Camera.main;

        public Vector3 WorldToScreenPoint(Vector3 worldPosition)
        {
            return _camera.WorldToScreenPoint(worldPosition);
        }
    }
}
