using Modules.Configs;
using Modules.ZooWorld._Project.Scripts.Runtime.Utils;
using UnityEngine;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Configs
{
    [CreateAssetMenu(fileName = "World Bounds Config", menuName = ProjectConstants.ProjectName + "/World Bounds Config")]
    public sealed class WorldBoundsConfig : Config
    {
        [SerializeField] private Vector3 _center = Vector3.zero;
        [SerializeField] private Vector3 _size = new Vector3(14f, 6f, 10f);

        public Bounds Bounds => new(_center, _size);
    }
}
