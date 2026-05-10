using JetBrains.Annotations;
using Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.Spawning;
using Zenject;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Infrastructure.Installers
{
    [UsedImplicitly]
    public sealed class ZooWorldSpawningInstaller : Installer<ZooWorldSpawningInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<AnimalSpawnRootProvider>().AsSingle();
            Container.Bind<IAnimalSpawnService>().To<AnimalSpawnService>().AsSingle();
        }
    }
}