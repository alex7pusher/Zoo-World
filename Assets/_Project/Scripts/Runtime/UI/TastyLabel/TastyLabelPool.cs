using System.Collections.Generic;
using JetBrains.Annotations;
using Modules.ZooWorld._Project.Scripts.Runtime.Configs;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace Modules.ZooWorld._Project.Scripts.Runtime.UI.TastyLabel
{
    [UsedImplicitly]
    public sealed class TastyLabelPool : ITastyLabelPool
    {
        private readonly TastyLabelConfig _config;
        private readonly List<TastyLabelItem> _activeLabels = new List<TastyLabelItem>();
        private readonly ObjectPool<TastyLabelItem> _pool;
        private bool _isDisposed;

        public TastyLabelPool(TastyLabelConfig config)
        {
            _config = config;
            _pool = new ObjectPool<TastyLabelItem>(
                createFunc: CreateLabel,
                actionOnGet: OnGet,
                actionOnRelease: OnRelease,
                actionOnDestroy: label =>
                {
                    if (label != null)
                    {
                        label.Destroy();
                    }
                },
                collectionCheck: true,
                defaultCapacity: _config.PoolDefaultCapacity,
                maxSize: _config.PoolMaxSize);
        }

        public TastyLabelItem Get()
        {
            if (_isDisposed)
            {
                return null;
            }

            TastyLabelItem label = _pool.Get();
            _activeLabels.Add(label);
            return label;
        }

        public void Release(TastyLabelItem label)
        {
            if (label == null || _isDisposed)
            {
                return;
            }

            _activeLabels.Remove(label);
            _pool.Release(label);
        }

        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            _isDisposed = true;
            for (int i = _activeLabels.Count - 1; i >= 0; i--)
            {
                TastyLabelItem label = _activeLabels[i];
                if (label != null)
                {
                    label.Destroy();
                }
            }

            _activeLabels.Clear();
            _pool.Clear();
        }

        private TastyLabelItem CreateLabel()
        {
            if (_config.LabelPrefab == null)
            {
                throw new System.InvalidOperationException($"{nameof(TastyLabelConfig)} has no label prefab assigned.");
            }

            TastyLabelItem label = Object.Instantiate(_config.LabelPrefab);
            label.SetActive(false);
            return label;
        }

        private void OnGet(TastyLabelItem label)
        {
            if (label == null)
            {
                return;
            }

            label.SetActive(true);
        }

        private void OnRelease(TastyLabelItem label)
        {
            if (label == null)
            {
                return;
            }

            label.SetActive(false);
        }
    }
}