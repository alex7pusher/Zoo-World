using Modules.Configs;
using Modules.ZooWorld._Project.Scripts.Runtime.Utils;
using UnityEngine;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Configs
{
    [CreateAssetMenu(fileName = "Spawn Config", menuName = ProjectConstants.ProjectName + "/Spawn Config")]
    public sealed class SpawnConfig : Config
    {
        [SerializeField] private Vector3 _center = Vector3.zero;
        [SerializeField] private Vector3 _size = new Vector3(12f, 0f, 8f);
        [SerializeField] private float _spawnHeight = 1f;
        [SerializeField] private Vector2 _spawnInterval = new Vector2(1f, 2f);

        public Vector3 GetRandomPoint()
        {
            float halfX = Mathf.Abs(_size.x) * 0.5f;
            float halfZ = Mathf.Abs(_size.z) * 0.5f;
            return new Vector3(
                _center.x + Random.Range(-halfX, halfX),
                _spawnHeight,
                _center.z + Random.Range(-halfZ, halfZ));
        }

        public float GetRandomSpawnDelay()
        {
            float min = Mathf.Min(_spawnInterval.x, _spawnInterval.y);
            float max = Mathf.Max(_spawnInterval.x, _spawnInterval.y);
            return Random.Range(Mathf.Max(0.05f, min), Mathf.Max(0.05f, max));
        }
    }
}
