using JetBrains.Annotations;
using Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.Animals;
using Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.Movement;
using Zenject;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Infrastructure.Installers
{
    [UsedImplicitly]
    public sealed class ZooWorldAnimalsInstaller : Installer<ZooWorldAnimalsInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IAnimalPool>().To<AnimalPool>().AsSingle();
            Container.Bind<IAnimalMovementFactory>().To<AnimalMovementFactory>().AsSingle();
            Container.Bind<IAnimalFactory>().To<AnimalFactory>().AsSingle();
        }
    }
}