using JetBrains.Annotations;
using Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.World;
using Zenject;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Infrastructure.Installers
{
    [UsedImplicitly]
    public sealed class ZooWorldWorldInstaller : Installer<ZooWorldWorldInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IWorldBounds>().To<WorldBounds>().AsSingle();
        }
    }
}