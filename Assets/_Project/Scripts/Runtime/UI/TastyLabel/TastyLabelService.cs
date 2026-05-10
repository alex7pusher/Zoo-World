using System;
using JetBrains.Annotations;
using Modules.ZooWorld._Project.Scripts.Runtime.Configs;
using Modules.ZooWorld._Project.Scripts.Runtime.Infrastructure.Events;
using UniRx;
using UnityEngine;

namespace Modules.ZooWorld._Project.Scripts.Runtime.UI.TastyLabel
{
    [UsedImplicitly]
    public sealed class TastyLabelService : ITastyLabelService, IDisposable
    {
        private readonly IZooEventBus _eventBus;
        private readonly TastyLabelConfig _config;
        private readonly ITastyLabelPool _labelPool;
        private readonly IScreenPositionService _screenPositionService;

        private IDisposable _animalEatenSubscription;

        public TastyLabelService(
            IZooEventBus eventBus,
            TastyLabelConfig config,
            ITastyLabelPool labelPool,
            IScreenPositionService screenPositionService)
        {
            _eventBus = eventBus;
            _config = config;
            _labelPool = labelPool;
            _screenPositionService = screenPositionService;
        }

        public void Initialize()
        {
            if (_animalEatenSubscription != null)
            {
                return;
            }

            _animalEatenSubscription = _eventBus.Receive<AnimalEatenEvent>().Subscribe(OnAnimalEaten);
        }

        public void Dispose()
        {
            _animalEatenSubscription?.Dispose();
            _animalEatenSubscription = null;
        }

        private void OnAnimalEaten(AnimalEatenEvent eventData)
        {
            if (eventData.Predator == null || eventData.Predator.View == null || eventData.Victim == null)
            {
                return;
            }

            if (!eventData.ShouldShowTastyLabel)
            {
                return;
            }

            TastyLabelItem label = _labelPool.Get();
            if (label == null)
            {
                return;
            }

            Vector3 labelPosition = eventData.Predator.View.LabelAnchor.position + Vector3.up * _config.Offset;
            Vector3 screenPosition = _screenPositionService.WorldToScreenPoint(labelPosition);
            label.Show(
                screenPosition,
                _config.FadeInDuration,
                _config.Lifetime,
                _config.FadeOutDuration,
                () => ReleaseLabel(label));
        }

        private void ReleaseLabel(TastyLabelItem label)
        {
            if (label == null)
            {
                return;
            }

            _labelPool.Release(label);
        }
    }
}