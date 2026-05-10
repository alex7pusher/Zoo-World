using JetBrains.Annotations;
using Modules.ZooWorld._Project.Scripts.Runtime.UI;
using Modules.ZooWorld._Project.Scripts.Runtime.UI.TastyLabel;
using Zenject;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Infrastructure.Installers
{
    [UsedImplicitly]
    public sealed class ZooWorldUiInstaller : Installer<ZooWorldUiInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<MainCameraScreenPositionService>().AsSingle();
            Container.BindInterfacesTo<TastyLabelPool>().AsSingle();
            Container.BindInterfacesAndSelfTo<TastyLabelService>().AsSingle();
        }
    }
}