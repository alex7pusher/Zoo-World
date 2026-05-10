namespace Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.Animals
{
    public interface IAnimalFactory
    {
        IAnimal Create(AnimalSpawnData spawnData);
    }
}
