using Modules.Configs;
using Modules.ZooWorld._Project.Scripts.Runtime.Utils;
using UnityEngine;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Configs
{
    [CreateAssetMenu(fileName = "Collision Config", menuName = ProjectConstants.ProjectName + "/Collision Config")]
    public sealed class CollisionConfig : Config
    {
        [SerializeField] [Min(0f)] private float _samePreyCollisionImpulse = 3f;

        public float SamePreyCollisionImpulse => Mathf.Max(0f, _samePreyCollisionImpulse);
    }
}
