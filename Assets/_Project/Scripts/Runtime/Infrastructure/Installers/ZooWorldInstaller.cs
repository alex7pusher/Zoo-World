using Zenject;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Infrastructure.Installers
{
    public sealed class ZooWorldInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            ZooWorldBootstrapInstaller.Install(Container);
            ZooWorldWorldInstaller.Install(Container);
            ZooWorldAnimalsInstaller.Install(Container);
            ZooWorldFoodChainInstaller.Install(Container);
            ZooWorldSpawningInstaller.Install(Container);
            ZooWorldUiInstaller.Install(Container);
        }
    }
}