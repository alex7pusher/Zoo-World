using System.Collections.Generic;
using JetBrains.Annotations;
using Modules.UISystem.Runtime.Scripts.Base;
using Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.Animals;
using UniRx;

namespace Modules.ZooWorld._Project.Scripts.Runtime.UI.ZooStats
{
    [UsedImplicitly]
    public sealed class ZooStatsPresenter : BaseUIPresenter<ZooStatsView, ZooStatsModel>
    {
        public override string UIPrefabAddressablesName => "ZooStatsView";

        public override void Initialize(UnityEngine.GameObject view, BaseUIModel model)
        {
            base.Initialize(view, model);
            View.InitializeCounters(Model.DeadCounts.Keys);
            
            foreach (KeyValuePair<AnimalChainType, IReadOnlyReactiveProperty<int>> deadCount in Model.DeadCounts)
            {
                AnimalChainType chainType = deadCount.Key;
                deadCount.Value
                         .Subscribe(count => View.SetCount(chainType, count))
                         .AddTo(View);
            }
        }
    }
}