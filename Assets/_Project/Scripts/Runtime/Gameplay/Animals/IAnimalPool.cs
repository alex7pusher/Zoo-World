namespace Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.Animals
{
    public interface IAnimalPool
    {
        AnimalView Get(AnimalSpawnData spawnData);
        void Release(IAnimal animal);
    }
}
