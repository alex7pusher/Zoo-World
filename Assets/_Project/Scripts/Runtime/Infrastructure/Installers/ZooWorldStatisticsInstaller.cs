using JetBrains.Annotations;
using Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.Statistics;
using Zenject;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Infrastructure.Installers
{
    [UsedImplicitly]
    public sealed class ZooWorldStatisticsInstaller : Installer<ZooWorldStatisticsInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ZooStatisticsService>().AsSingle();
        }
    }
}