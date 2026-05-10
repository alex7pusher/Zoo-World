using JetBrains.Annotations;
using Modules.Bootstrap;
using Modules.Bootstrap.Interfaces;
using Modules.ZooWorld._Project.Scripts.Runtime.Infrastructure.Bootstrap;
using Zenject;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Infrastructure.Installers
{
    [UsedImplicitly]
    public sealed class ZooWorldBootstrapInstaller : Installer<ZooWorldBootstrapInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<Bootstrapper>().AsSingle();
            Container.Bind<IBootstrapStage>().To<AnimalSpawnBootstrapStage>().AsSingle();
            Container.Bind<IBootstrapStage>().To<ZooBootstrapStage>().AsSingle();
            Container.Bind<ZooWorldSimulation>().AsSingle();
        }
    }
}