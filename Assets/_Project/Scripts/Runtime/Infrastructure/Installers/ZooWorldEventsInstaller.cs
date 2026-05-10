using JetBrains.Annotations;
using Modules.ZooWorld._Project.Scripts.Runtime.Infrastructure.Events;
using Zenject;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Infrastructure.Installers
{
    [UsedImplicitly]
    public sealed class ZooWorldEventsInstaller : Installer<ZooWorldEventsInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ZooEventBus>().AsSingle();
        }
    }
}