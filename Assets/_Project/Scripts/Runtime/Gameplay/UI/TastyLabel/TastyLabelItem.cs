using System;
using DG.Tweening;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.UI.TastyLabel
{
    public sealed class TastyLabelItem : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;

        private Sequence _currentSequence;

        private void Awake()
        {
            if (_rectTransform == null)
            {
                _rectTransform = transform as RectTransform;
            }
        }

        public void Show(
            Vector3 screenPosition,
            float fadeInDuration,
            float displayDuration,
            float fadeOutDuration,
            Action onComplete)
        {
            _currentSequence?.Kill(false);
            _rectTransform.position = screenPosition;
            _rectTransform.localScale = Vector3.zero;

            _currentSequence = DOTween.Sequence();
            _currentSequence.SetLink(gameObject, LinkBehaviour.KillOnDestroy);
            _currentSequence.Append(_rectTransform.DOScale(Vector3.one, fadeInDuration));
            _currentSequence.AppendInterval(displayDuration);
            _currentSequence.Append(_rectTransform.DOScale(Vector3.zero, fadeOutDuration));
            _currentSequence.OnComplete(() => onComplete?.Invoke());
        }

        public void SetActive(bool isActive)
        {
            if (!isActive)
            {
                _currentSequence?.Kill();
                if (_rectTransform != null)
                {
                    _rectTransform.localScale = Vector3.zero;
                }
            }

            gameObject.SetActive(isActive);
        }

        public void Destroy()
        {
            _currentSequence?.Kill(false);

            if (gameObject != null)
            {
                Object.Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            _currentSequence?.Kill(false);
            _currentSequence = null;
        }
    }
}