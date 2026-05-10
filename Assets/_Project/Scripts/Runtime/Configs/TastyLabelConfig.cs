using Modules.Configs;
using Modules.ZooWorld._Project.Scripts.Runtime.UI.TastyLabel;
using Modules.ZooWorld._Project.Scripts.Runtime.Utils;
using UnityEngine;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Configs
{
    [CreateAssetMenu(fileName = "Tasty Label Config", menuName = ProjectConstants.ProjectName + "/Tasty Label Config")]
    public sealed class TastyLabelConfig : Config
    {
        [SerializeField] private float _lifetime = 1f;
        [SerializeField] private float _fadeInDuration = 0.2f;
        [SerializeField] private float _fadeOutDuration = 0.3f;
        [SerializeField] private float _offset = 0.5f;
        [SerializeField] private int _poolDefaultCapacity = 5;
        [SerializeField] private int _poolMaxSize = 20;
        [SerializeField] private TastyLabelItem _labelPrefab;

        public float Lifetime => Mathf.Max(0.1f, _lifetime);
        public float FadeInDuration => Mathf.Max(0f, _fadeInDuration);
        public float FadeOutDuration => Mathf.Max(0f, _fadeOutDuration);
        public float Offset => _offset;
        public int PoolDefaultCapacity => Mathf.Max(1, _poolDefaultCapacity);
        public int PoolMaxSize => Mathf.Max(PoolDefaultCapacity, _poolMaxSize);
        public TastyLabelItem LabelPrefab => _labelPrefab;

        public override void Validate()
        {
            if (_labelPrefab == null)
            {
                Debug.LogError($"{nameof(TastyLabelConfig)} has no label prefab.", this);
            }
        }
    }
}