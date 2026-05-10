using JetBrains.Annotations;
using Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.FoodChain;
using Zenject;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Infrastructure.Installers
{
    [UsedImplicitly]
    public sealed class ZooWorldFoodChainInstaller : Installer<ZooWorldFoodChainInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IFoodChainCollisionStrategy>().To<PreyFoodChainCollisionStrategy>().AsSingle();
            Container.Bind<IFoodChainCollisionStrategy>().To<PredatorFoodChainCollisionStrategy>().AsSingle();
            Container.Bind<IFoodChainResolver>().To<FoodChainResolver>().AsSingle();
            Container.Bind<IAnimalDeathService>().To<AnimalDeathService>().AsSingle();
            Container.Bind<IAnimalCollisionHandler>().To<AnimalCollisionHandler>().AsSingle();
        }
    }
}